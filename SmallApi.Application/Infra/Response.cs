using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SmallApi.Application.Infra
{
    public class Response
    {
        private readonly IList<string> _messages = new List<string>();

        public IEnumerable<string> Errors { get; }

        public object Content { get; set; }

        public string SuccessMessage { get; set; }

        public bool Success { get { return this._messages.Count == 0; } }

        public Response() => Errors = new ReadOnlyCollection<string>(_messages);

        public Response AddError(string message)
        {
            _messages.Add(message);
            return this;
        }
    }
}
