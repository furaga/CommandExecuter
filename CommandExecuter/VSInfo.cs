using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;
using Microsoft.VisualStudio.Text.Formatting;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;

namespace FLib
{
    [Export(typeof(IWpfTextViewCreationListener))]
    [ContentType("CSharp")]
    [TextViewRole(PredefinedTextViewRoles.Document)]
    internal sealed class VSInfo : IWpfTextViewCreationListener
    {
        public static IWpfTextView WpfTextView { get { return _view; } }
        public static DTE DTE { get { return _dte; } }
        public static DTE2 DTE2 { get { return _dte2; } }
        public static Debugger Debugger { get { return _debugger; } }
        public static Debugger2 Debugger2 { get { return _debugger2; } }

        private static IWpfTextView _view;
        private static DTE _dte;
        private static DTE2 _dte2;
        private static Debugger _debugger;
        private static Debugger2 _debugger2;

        [Import]
        internal SVsServiceProvider ServiceProvider = null;

        public void TextViewCreated(IWpfTextView textView)
        {
            _view = textView;
            _dte = (DTE)ServiceProvider.GetService(typeof(DTE)) as DTE;
            _dte2 = (DTE2)ServiceProvider.GetService(typeof(DTE)) as DTE2;
            _debugger = _dte.Debugger;
            _debugger2 = _dte2.Debugger as Debugger2;



            if (DTE2 != null)
            {
                foreach (Command cmd in DTE2.Commands)
                {
                    if (cmd.Name.EndsWith(".CommandExecution"))
                        continue;
                    var bindings = cmd.Bindings as object[];
                    if (bindings != null && bindings.Length >= 1)
                    {
                        var bind = bindings[0];
                        var keys = bind as string;
                        // Riskey: 既存の Ctrl + J のコマンドがあった場合、そのキーバインドを削除する。
                        // 作者が使う機能でCtrl + Jを割り当てているコマンドはないので実用上問題ないが。。。
                        // 参考: VisualStudioの定義済みショートカットキー一覧　http://msdn.microsoft.com/ja-jp/library/da5kh0wa.aspx
                        // デフォルトだとカーソルが属すクラスのメンバ変数が列挙される
                        if (keys != null && keys.ToLower().EndsWith("::ctrl+j"))
                            cmd.Bindings = new object[0];
                    }
                }
            }
        }

    }
}