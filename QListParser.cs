using System;
using System.Collections.Generic;

namespace QList
{
    public class QListParser
    {
        private string _input;
        private int _position;

        public QListObject Parse(string input)
        {
            _input = input;
            _position = 0;
            SkipWhitespace();
            return ParseObject();
        }

        private QListObject ParseObject()
        {
            QListObject obj = new QListObject();
            Expect('{');

            while (true)
            {
                SkipWhitespace();
                if (Peek() == '}')
                {
                    _position++;
                    break;
                }

                string key = ParseKey();
                SkipWhitespace();
                Expect('=');
                SkipWhitespace();
                object value = ParseValue();
                SkipWhitespace();
                Expect(';');
                SkipWhitespace();

                obj[key] = value;
            }

            return obj;
        }

        private string ParseKey()
        {
            int start = _position;
            while (!IsAtEnd() && (char.IsLetterOrDigit(Peek()) || Peek() == '_'))
            {
                _position++;
            }
            return _input.Substring(start, _position - start);
        }

        private object ParseValue()
        {
            char c = Peek();
            if (c == '{') return ParseObject();
            if (c == '[') return ParseList();
            if (c == '"') return ParseString();
            return ParsePrimitive();
        }

        private List<object> ParseList()
        {
            List<object> list = new List<object>();
            Expect('[');
            SkipWhitespace();

            while (Peek() != ']')
            {
                list.Add(ParseValue());
                SkipWhitespace();

                if (Peek() == ',')
                {
                    _position++;
                    SkipWhitespace();
                }
            }

            Expect(']');
            return list;
        }

        private string ParseString()
        {
            Expect('"');
            int start = _position;

            while (Peek() != '"')
            {
                _position++;
            }

            string result = _input.Substring(start, _position - start);
            Expect('"');
            return result;
        }

        private object ParsePrimitive()
        {
            int start = _position;

            while (!IsAtEnd() && (char.IsLetterOrDigit(Peek()) || Peek() == '.'))
            {
                _position++;
            }

            string token = _input.Substring(start, _position - start);

            if (int.TryParse(token, out int intValue)) return intValue;
            if (double.TryParse(token, out double doubleValue)) return doubleValue;

            if (token == "true") return true;
            if (token == "false") return false;

            return token;
        }

        private void SkipWhitespace()
        {
            while (!IsAtEnd() && char.IsWhiteSpace(Peek()))
                _position++;
        }

        private void Expect(char expected)
        {
            if (IsAtEnd() || _input[_position] != expected)
                throw new Exception($"Expected '{expected}' at position {_position}");
            _position++;
        }

        private char Peek() => _input[_position];
        private bool IsAtEnd() => _position >= _input.Length;
    }
}
