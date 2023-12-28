using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using ReactiveUI;
using System.Linq.Expressions;
using System.Reactive;
using System.Windows.Input;
using Telerik.Blazor.Components;
using Telerik.DataSource;
using System.Reactive.Linq;

namespace UN.CYBERCOM.Web.Helpers
{
    public static class EventsToCommand
    {
        public static EventCallback<T> BindCommand<T>(this ICommand command, object? parameter = null)
        {
            MulticastDelegate m1 = () => command.Execute(parameter);
            return new EventCallback<T>(null, m1);
        }
        public static EventCallback<IEnumerable<T>> BindSelectCommand<T>(this ICommand command, object reciever)
        {
            return EventCallback.Factory.Create<IEnumerable<T>>(reciever, args =>
            {
                command.Execute(args);
            });
        }
        public static EventCallback<GridCommandEventArgs> BindEditCommand<TEntity, TKey>(this ICommand command, object reciever)
        {
            return EventCallback.Factory.Create<GridCommandEventArgs>(reciever, args =>
            {
                command.Execute(args.Item);
            });
        }
        public static EventCallback<object> BindSelectCommand<TKey>(this ReactiveCommand<TKey, Unit> command, object reciever)
        {
            return EventCallback.Factory.Create<object>(reciever, async args =>
            {
                if (args != null)
                    await command.Execute((TKey)args).GetAwaiter();
            });
        }
        public static EventCallback<FileSelectEventArgs> BindUploadCommand(this ReactiveCommand<byte[], Unit> command, object reciever)
        {
            return EventCallback.Factory.Create<FileSelectEventArgs>(reciever, async args =>
            {
                try
                {
                    var file = args.Files.Single();
                    var buffer = new byte[file.Stream.Length];
                    await file.Stream.ReadAsync(buffer);
                    await command.Execute(buffer).GetAwaiter();
                }
                catch (Exception ex)
                {
                    throw;
                }
            });
        }
        public static EventCallback<InputFileChangeEventArgs> BindUploadBuiltInCommand(this ReactiveCommand<byte[], Unit> command, object reciever)
        {
            return EventCallback.Factory.Create<InputFileChangeEventArgs>(reciever, async args =>
            {
                try
                {
                    var file = args.File;
                    var stream = file.OpenReadStream(1000000000);
                    using var ms = new MemoryStream();
                    await stream.CopyToAsync(ms);
                    await command.Execute(ms.ToArray()).GetAwaiter();
                }
                catch (Exception ex)
                {
                    throw;
                }
            });
        }
    }
}
