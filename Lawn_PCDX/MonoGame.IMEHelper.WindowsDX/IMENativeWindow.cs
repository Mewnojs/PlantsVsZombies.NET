using Microsoft.Xna.Framework;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MonoGame.IMEHelper
{
    /// <summary>
    /// Native window class that handles IME.
    /// </summary>
    internal sealed class IMENativeWindow : NativeWindow, IDisposable
    {
        private WinFormsIMEHandler _imeHandler;

        private IMMCompositionString
            _compstr, _compclause, _compattr,
            _compread, _compreadclause, _compreadattr,
            _resstr, _resclause,
            _resread, _resreadclause;
        private IMMCompositionInt _compcurpos;

        private bool _disposed, _showIMEWin;

        private IntPtr _context;

        /// <summary>
        /// Gets the state if the IME should be enabled
        /// </summary>
        public bool IsEnabled { get; private set; }

        public bool IsIMEOpen { get; private set; }

        /// <summary>
        /// Composition String
        /// </summary>
        public string CompositionString { get { return _compstr.ToString(); } }

        /// <summary>
        /// Composition Clause
        /// </summary>
        public string CompositionClause { get { return _compclause.ToString(); } }

        /// <summary>
        /// Composition String Reads
        /// </summary>
        public string CompositionReadString { get { return _compread.ToString(); } }

        /// <summary>
        /// Composition Clause Reads
        /// </summary>
        public string CompositionReadClause { get { return _compreadclause.ToString(); } }

        /// <summary>
        /// Result String
        /// </summary>
        public string ResultString { get { return _resstr.ToString(); } }

        /// <summary>
        /// Result Clause
        /// </summary>
        public string ResultClause { get { return _resclause.ToString(); } }

        /// <summary>
        /// Result String Reads
        /// </summary>
        public string ResultReadString { get { return _resread.ToString(); } }

        /// <summary>
        /// Result Clause Reads
        /// </summary>
        public string ResultReadClause { get { return _resreadclause.ToString(); } }

        /// <summary>
        /// Caret position of the composition
        /// </summary>
        public int CompositionCursorPos { get { return _compcurpos.Value; } }

        /// <summary>
        /// Array of the candidates
        /// </summary>
        public string[] Candidates { get; private set; }

        /// <summary>
        /// First candidate index of current page
        /// </summary>
        public uint CandidatesPageStart { get; private set; }

        /// <summary>
        /// How many candidates should display per page
        /// </summary>
        public uint CandidatesPageSize { get; private set; }

        /// <summary>
        /// The selected canddiate index
        /// </summary>
        public uint CandidatesSelection { get; private set; }

        /// <summary>
        /// Get the composition attribute at character index.
        /// </summary>
        /// <param name="index">Character Index</param>
        /// <returns>Composition Attribute</returns>
        public CompositionAttributes GetCompositionAttr(int index)
        {
            return (CompositionAttributes)_compattr[index];
        }

        /// <summary>
        /// Get the composition read attribute at character index.
        /// </summary>
        /// <param name="index">Character Index</param>
        /// <returns>Composition Attribute</returns>
        public CompositionAttributes GetCompositionReadAttr(int index)
        {
            return (CompositionAttributes)_compreadattr[index];
        }

        /// <summary>
        /// Constructor, must be called when the window create.
        /// </summary>
        /// <param name="handle">Handle of the window</param>
        /// <param name="showDefaultIMEWindow">True if you want to display the default IME window</param>
        internal IMENativeWindow(WinFormsIMEHandler imeHandler, IntPtr handle, bool showDefaultIMEWindow = false)
        {
            this._imeHandler = imeHandler; 

            this._context = IntPtr.Zero;
            this.Candidates = new string[0];
            this._compcurpos = new IMMCompositionInt(IMM.GCSCursorPos);
            this._compstr = new IMMCompositionString(IMM.GCSCompStr);
            this._compclause = new IMMCompositionString(IMM.GCSCompClause);
            this._compattr = new IMMCompositionString(IMM.GCSCompAttr);
            this._compread = new IMMCompositionString(IMM.GCSCompReadStr);
            this._compreadclause = new IMMCompositionString(IMM.GCSCompReadClause);
            this._compreadattr = new IMMCompositionString(IMM.GCSCompReadAttr);
            this._resstr = new IMMCompositionString(IMM.GCSResultStr);
            this._resclause = new IMMCompositionString(IMM.GCSResultClause);
            this._resread = new IMMCompositionString(IMM.GCSResultReadStr);
            this._resreadclause = new IMMCompositionString(IMM.GCSResultReadClause);
            this._showIMEWin = showDefaultIMEWindow;

            AssignHandle(handle);
        }

        /// <summary>
        /// Enable the IME
        /// </summary>
        public void EnableIME()
        {
            IsEnabled = true;

            IMM.DestroyCaret();
            IMM.CreateCaret(Handle, IntPtr.Zero, 1, 1);

            _context = IMM.ImmGetContext(Handle);
            if (_context != IntPtr.Zero)
            {
                IMM.ImmAssociateContext(Handle, _context);
                IMM.ImmReleaseContext(Handle, _context);
                return;
            }

            // This fix the bug that _context is 0 on fullscreen mode.
            ImeContext.Enable(Handle);
        }

        /// <summary>
        /// Disable the IME
        /// </summary>
        public void DisableIME()
        {
            IsEnabled = false;

            IMM.DestroyCaret();

            IMM.ImmAssociateContext(Handle, IntPtr.Zero);
        }

        public void SetTextInputRect(ref Rectangle rect)
        {
            _context = IMM.ImmGetContext(Handle);

            var candidateForm = new IMM.CandidateForm(new IMM.Point(rect.X, rect.Y));
            IMM.ImmSetCandidateWindow(_context, ref candidateForm);
            IMM.SetCaretPos(rect.X, rect.Y);

            IMM.ImmReleaseContext(Handle, _context);
        }

        /// <summary>
        /// Dispose everything
        /// </summary>
        public void Dispose()
        {
            if (!_disposed)
            {
                ReleaseHandle();
                _disposed = true;
            }
        }

        protected override void WndProc(ref Message msg)
        {
            switch (msg.Msg)
            {
                case IMM.ImeSetContext:
                    if (msg.WParam.ToInt32() == 1)
                    {
                        if (IsEnabled)
                            EnableIME();
                        if (!_showIMEWin)
                            msg.LParam = (IntPtr)0;
                    }
                    break;
                case IMM.InputLanguageChange:
                    return;
                case IMM.ImeNotify:
                    IMENotify(msg.WParam.ToInt32());
                    if (!_showIMEWin) return;
                    break;
                case IMM.ImeStartCompostition:
                    IMEStartComposion(msg.LParam.ToInt32());
                    return;
                case IMM.ImeComposition:
                    IMESetContextForAll();
                    IMEComposition(msg.LParam.ToInt32());
                    IMM.ImmReleaseContext(Handle, _context);
                    break;
                case IMM.ImeEndComposition:
                    IMESetContextForAll();
                    IMEEndComposition(msg.LParam.ToInt32());
                    IMM.ImmReleaseContext(Handle, _context);
                    if (!_showIMEWin) return;
                    break;
                case IMM.Char:
                    CharEvent(msg.WParam.ToInt32());
                    break;
            }
            base.WndProc(ref msg);
        }

        private void ClearComposition()
        {
            _compstr.Clear();
            _compclause.Clear();
            _compattr.Clear();
            _compread.Clear();
            _compreadclause.Clear();
            _compreadattr.Clear();
        }

        private void ClearResult()
        {
            _resstr.Clear();
            _resclause.Clear();
            _resread.Clear();
            _resreadclause.Clear();
        }

        #region IME Message Handlers

        private void IMESetContextForAll()
        {
            _context = IMM.ImmGetContext(Handle);

            _compcurpos.IMEHandle = _context;
            _compstr.IMEHandle = _context;
            _compclause.IMEHandle = _context;
            _compattr.IMEHandle = _context;
            _compread.IMEHandle = _context;
            _compreadclause.IMEHandle = _context;
            _compreadattr.IMEHandle = _context;
            _resstr.IMEHandle = _context;
            _resclause.IMEHandle = _context;
            _resread.IMEHandle = _context;
            _resreadclause.IMEHandle = _context;
        }

        private void IMENotify(int WParam)
        {
            switch (WParam)
            {
                case IMM.ImnSetOpenStatus:
                    _context = IMM.ImmGetContext(Handle);
                    IsIMEOpen = IMM.ImmGetOpenStatus(_context);
                    System.Diagnostics.Trace.WriteLine(string.Format("IsIMEOpen: {0}", IsIMEOpen ? "True" : "False"));
                    break;
                case IMM.ImnOpenCandidate:
                case IMM.ImnChangeCandidate:
                    IMEChangeCandidate();
                    break;
                case IMM.ImnCloseCandidate:
                    IMECloseCandidate();
                    break;
                case IMM.ImnPrivate:
                    break;
                default:
                    break;
            }
        }

        private void IMEChangeCandidate()
        {
            _context = IMM.ImmGetContext(Handle);

            uint length = IMM.ImmGetCandidateList(_context, 0, IntPtr.Zero, 0);
            if (length > 0)
            {
                IntPtr pointer = Marshal.AllocHGlobal((int)length);
                length = IMM.ImmGetCandidateList(_context, 0, pointer, length);
                IMM.CandidateList cList = (IMM.CandidateList)Marshal.PtrToStructure(pointer, typeof(IMM.CandidateList));

                CandidatesSelection = cList.dwSelection;
                CandidatesPageStart = cList.dwPageStart;
                CandidatesPageSize = cList.dwPageSize;

                if (cList.dwCount > 1)
                {
                    Candidates = new string[cList.dwCount];
                    for (int i = 0; i < cList.dwCount; i++)
                    {
                        int sOffset = Marshal.ReadInt32(pointer, 24 + 4 * i);
                        Candidates[i] = Marshal.PtrToStringUni(pointer + sOffset);
                    }

                    _imeHandler.OnTextComposition(CompositionString, CompositionCursorPos, new CandidateList {
                        Candidates = Candidates,
                        CandidatesPageStart = CandidatesPageStart,
                        CandidatesPageSize = CandidatesPageSize,
                        CandidatesSelection = CandidatesSelection
                    });
                }
                else
                    IMECloseCandidate();

                Marshal.FreeHGlobal(pointer);
            }

            IMM.ImmReleaseContext(Handle, _context);
        }

        private void IMECloseCandidate()
        {
            CandidatesSelection = CandidatesPageStart = CandidatesPageSize = 0;
            Candidates = new string[0];

            _imeHandler.OnTextComposition(CompositionString, CompositionCursorPos, null);
        }

        private void IMEStartComposion(int lParam)
        {
            ClearComposition();
            ClearResult();

            _imeHandler.OnTextComposition(string.Empty, 0);
        }

        private void IMEComposition(int lParam)
        {
            if (_compstr.Update(lParam))
            {
                _compclause.Update();
                _compattr.Update();
                _compread.Update();
                _compreadclause.Update();
                _compreadattr.Update();
                _compcurpos.Update();

                _imeHandler.OnTextComposition(CompositionString, CompositionCursorPos);
            }
        }

        private void IMEEndComposition(int lParam)
        {
            ClearComposition();

            if (_resstr.Update(lParam))
            {
                _resclause.Update();
                _resread.Update();
                _resreadclause.Update();

                _imeHandler.OnTextComposition(string.Empty, 0);
            }
        }

        private void CharEvent(int wParam)
        {
            var charInput = (char)wParam;

            var key = Microsoft.Xna.Framework.Input.Keys.None;
            if (!char.IsSurrogate(charInput))
                key = (Microsoft.Xna.Framework.Input.Keys) (IMM.VkKeyScanEx(charInput, InputLanguage.CurrentInputLanguage.Handle) & 0xff);

            _imeHandler.OnTextInput(charInput, key);

            if (IsEnabled)
                IMECloseCandidate();
        }

        #endregion
    }
}
