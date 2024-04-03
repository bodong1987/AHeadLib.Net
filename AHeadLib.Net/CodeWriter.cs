using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHeadLib.Net
{
    /// <summary>
    /// Enum ECodeWriterSaveResult
    /// </summary>
    public enum ECodeWriterSaveResult
    {
        /// <summary>
        /// The failure
        /// </summary>
        Failure,
        /// <summary>
        /// The success
        /// </summary>
        Success,
        /// <summary>
        /// The ignore when no changes
        /// </summary>
        IgnoreWhenNoChanges
    }


    /// <summary>
    /// Class CodeWriter.
    /// Help you write C++/C# codes
    /// </summary>
    public class CodeWriter
    {
        /// <summary>
        /// Gets the target path.
        /// </summary>
        /// <value>The target path.</value>
        public string? TargetPath { get; private set; }

        /// <summary>
        /// The buffer
        /// </summary>
        private StringBuilder Buffer = new StringBuilder();

        /// <summary>
        /// The indent level
        /// </summary>
        private int IndentLevel = 0;

        /// <summary>
        /// The z fast access string
        /// </summary>
        private static string[] Z_FastAccessString = new string[]{
            (""),
            ("    "),
            ("        "),
            ("            "),
            ("                "),
            ("                    "),
            ("                        "),
            ("                            "),
            ("                                "),
            ("                                    "),
            ("                                        ")
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeWriter" /> class.
        /// </summary>
        public CodeWriter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeWriter" /> class.
        /// </summary>
        /// <param name="targetPath">The target path.</param>
        public CodeWriter(string targetPath)
        {
            TargetPath = targetPath;
        }

        /// <summary>
        /// Begins the ident.
        /// </summary>
        /// <param name="withBrace">if set to <c>true</c> [with brace].</param>
        public void BeginIdent(bool withBrace)
        {
            if (withBrace)
            {
                WriteLeftBrace();
            }

            ++IndentLevel;
        }

        /// <summary>
        /// Ends the ident.
        /// </summary>
        /// <param name="withBrace">if set to <c>true</c> [with brace].</param>
        /// <param name="withSemicolon">if set to <c>true</c> [with semicolon].</param>
        public void EndIdent(bool withBrace, bool withSemicolon)
        {
            --IndentLevel;

            if (withBrace)
            {
                WriteRightBrace(withSemicolon);
            }
        }

        /// <summary>
        /// Gets the scoped space.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetScopedSpace()
        {
            if (IndentLevel >= 0 && IndentLevel < Z_FastAccessString.Length)
            {
                return Z_FastAccessString[IndentLevel];
            }

            return "".PadRight(IndentLevel * 4);
        }

        /// <summary>
        /// Writes the indent.
        /// </summary>
        private void WriteIndent()
        {
            Buffer.Append(GetScopedSpace());
        }

        /// <summary>
        /// Writes the right brace.
        /// </summary>
        /// <param name="withSemicolon">if set to <c>true</c> [with semicolon].</param>
        private void WriteRightBrace(bool withSemicolon)
        {
            if (withSemicolon)
            {
                Write("};");
            }
            else
            {
                Write("}");
            }
        }

        /// <summary>
        /// Writes the left brace.
        /// </summary>
        private void WriteLeftBrace()
        {
            Write("{");
        }

        /// <summary>
        /// Writes the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        public void Write(string text)
        {
            WriteIndent();
            Buffer.Append(text);
            WriteNewLine();
        }

        /// <summary>
        /// Writes the new line.
        /// </summary>
        public void WriteNewLine()
        {
            Buffer.Append("\n");
        }

        /// <summary>
        /// Writes the property.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="padding">The padding.</param>
        public void WriteProperty(string type, string name, string defaultValue, int padding)
        {
            Write(string.Format("{0} {1}{2}{3}",
                type.PadRight(padding),
                name,
                !string.IsNullOrEmpty(defaultValue) ? "=" : "",
                !string.IsNullOrEmpty(defaultValue) ? defaultValue : ""
                )
            );
        }

        /// <summary>
        /// The reserve symbols
        /// </summary>
        private static KeyValuePair<string, string>[] ReserveSymbols = new KeyValuePair<string, string>[]
        {
            new KeyValuePair<string, string>("<see cref", "__see_ref_symbol__"),
            new KeyValuePair<string, string>("/>", "__right_gt_symbol__")
        };

        /// <summary>
        /// Writes C# style comments
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="attribute">The attribute.</param>
        public void WriteComment(string comment, string tag = "summary", string attribute = "")
        {
            if (comment.IsNotNullOrEmpty() || attribute.IsNotNullOrEmpty())
            {
                foreach (var r in ReserveSymbols)
                {
                    comment = comment.Replace(r.Key, r.Value);
                }

                // remove xml special character                
                comment = comment.Replace("&", "&amp;");
                comment = comment.Replace("<", "&lt;");
                comment = comment.Replace(">", "&gt;");
                //comment = comment.Replace("'", "&apos;");
                //comment = comment.Replace("\"", "&quot;");

                foreach (var r in ReserveSymbols)
                {
                    comment = comment.Replace(r.Value, r.Key);
                }

                var lines = comment.Split('\n').ToList();
                lines.RemoveAll(x => x.Trim().IsNullOrEmpty());

                if (lines.Count > 1 || tag.iEquals("summary"))
                {
                    Write($"/// <{tag}{(attribute.IsNullOrEmpty() ? "" : " " + attribute)}>");

                    foreach (var text in lines)
                    {
                        Write($"/// {text.Trim()}");
                    }

                    Write($"/// </{tag}>");
                }
                else
                {
                    if (lines.Count <= 0 || lines[0].IsNullOrEmpty())
                    {
                        Write($"/// <{tag}{(attribute.IsNullOrEmpty() ? "" : " " + attribute)}/>");
                    }
                    else
                    {
                        Write($"/// <{tag}{(attribute.IsNullOrEmpty() ? "" : " " + attribute)}>{lines[0]}</{tag}>");
                    }

                }
            }
        }

        /// <summary>
        /// Writes the common comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        public void WriteCommonComment(string comment)
        {
            if (comment.IsNotNullOrEmpty())
            {
                var lines = comment.Split('\n').ToList();
                lines.RemoveAll(x => x.Trim().IsNullOrEmpty());

                foreach (var text in lines)
                {
                    Write($"// {text.Trim()}");
                }
            }
        }

        /// <summary>
        /// Saves the specified target path.
        /// </summary>
        /// <param name="targetPath">The target path.</param>
        /// <param name="forceSave">if set to <c>true</c> [force save].</param>
        /// <returns>ECodeWriterSaveResult.</returns>
        public ECodeWriterSaveResult Save(string targetPath, bool forceSave = false)
        {
            TargetPath = targetPath;
            return Save(forceSave);
        }

        /// <summary>
        /// Saves the specified force save.
        /// </summary>
        /// <param name="forceSave">if set to <c>true</c> [force save].</param>
        /// <returns>ECodeWriterSaveResult.</returns>
        public virtual ECodeWriterSaveResult Save(bool forceSave = false)
        {
            System.Diagnostics.Debug.Assert(TargetPath != null);

            if (string.IsNullOrEmpty(TargetPath))
            {
                return ECodeWriterSaveResult.Failure;
            }

            // check is threre any changed?
            string text = Buffer.ToString();
            byte[] utf8Codes = Encoding.UTF8.GetBytes(text);
            if (!forceSave && File.Exists(TargetPath))
            {
                byte[] existsBytes = File.ReadAllBytes(TargetPath!);

                if (ByteArraysEqual(existsBytes, utf8Codes))
                {
                    // same file, skip write
                    return ECodeWriterSaveResult.IgnoreWhenNoChanges;
                }
            }

            File.WriteAllBytes(TargetPath!, utf8Codes);

            return ECodeWriterSaveResult.Success;
        }

        private bool ByteArraysEqual(byte[] existsBytes, byte[] utf8Codes)
        {
            if(existsBytes.Length != utf8Codes.Length)
            {
                return false;
            }

            for(int i =0; i<existsBytes.Length; ++i)
            {
                if (existsBytes[i] != utf8Codes[i])
                {
                    return false;
                }
            }

            return true;
        }
    }

    /// <summary>
    /// Class ScopedCodeWriter.
    /// Implements the <see cref="System.IDisposable" />
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    class ScopedCodeWriter : IDisposable
    {
        /// <summary>
        /// The writer
        /// </summary>
        readonly CodeWriter Writer;
        /// <summary>
        /// The with brace
        /// </summary>
        readonly bool WithBrace, WithSemicolon;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScopedCodeWriter" /> class.
        /// </summary>
        /// <param name="codeWriter">The code writer.</param>
        /// <param name="withBrace">if set to <c>true</c> [with brace].</param>
        /// <param name="withSemicolon">if set to <c>true</c> [with semicolon].</param>
        public ScopedCodeWriter(CodeWriter codeWriter, bool withBrace = true, bool withSemicolon = false)
        {
            Writer = codeWriter;
            Writer.BeginIdent(withBrace);
            WithBrace = withBrace;
            WithSemicolon = withSemicolon;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Writer.EndIdent(WithBrace, WithSemicolon);
        }
    }
}
