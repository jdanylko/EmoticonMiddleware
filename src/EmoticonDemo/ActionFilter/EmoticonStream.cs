using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EmoticonDemo.ActionFilter
{
    public class EmoticonStream : Stream
    {
        private readonly Stream _base;
        StringBuilder _s = new StringBuilder();

        private List<KeyValuePair<string, string>> _emoticons = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>(@":-)", "smile.png"),
            new KeyValuePair<string, string>(@":)", "smile.png"),
            new KeyValuePair<string, string>(@";-)", "wink.png")
        };

        public EmoticonStream(Stream responseStream)
        {
            if (responseStream == null)
                throw new ArgumentNullException("responseStream");

            _base = responseStream;
        }
        public override void Flush()
        {
            _base.Flush();
        }
        public override bool CanRead => _base.CanRead;
        public override bool CanSeek => _base.CanSeek;
        public override bool CanWrite => _base.CanWrite;
        public override long Length => _base.Length;

        public override long Position
        {
            get { return _base.Position; }
            set { _base.Position = value; }
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            var html = Encoding.UTF8.GetString(buffer, offset, count);
            foreach (var emoticon in _emoticons)
            {
                if (!html.Contains(emoticon.Key)) continue;

                var image = $"<img src=\"/images/{emoticon.Value}\" />";
                html = html.Replace(emoticon.Key, image);
            }
            buffer = Encoding.UTF8.GetBytes(html);

            _base.Write(buffer, 0, buffer.Length);
        }

        public override void SetLength(long value)
        {
            _base.SetLength(value);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _base.Seek(offset, origin);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _base.Read(buffer, offset, count);
        }
    }
}