
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using MessagePack;
using NvimClient.NvimMsgpack.Models;

namespace NvimClient.API
{
  public partial class NvimAPI
  {
    public event EventHandler<ModeInfoSetEventArgs> ModeInfoSetEvent;
    public event EventHandler UpdateMenuEvent;
    public event EventHandler BusyStartEvent;
    public event EventHandler BusyStopEvent;
    public event EventHandler MouseOnEvent;
    public event EventHandler MouseOffEvent;
    public event EventHandler<ModeChangeEventArgs> ModeChangeEvent;
    public event EventHandler BellEvent;
    public event EventHandler VisualBellEvent;
    public event EventHandler FlushEvent;
    public event EventHandler SuspendEvent;
    public event EventHandler<SetTitleEventArgs> SetTitleEvent;
    public event EventHandler<SetIconEventArgs> SetIconEvent;
    public event EventHandler<ScreenshotEventArgs> ScreenshotEvent;
    public event EventHandler<OptionSetEventArgs> OptionSetEvent;
    public event EventHandler<UpdateFgEventArgs> UpdateFgEvent;
    public event EventHandler<UpdateBgEventArgs> UpdateBgEvent;
    public event EventHandler<UpdateSpEventArgs> UpdateSpEvent;
    public event EventHandler<ResizeEventArgs> ResizeEvent;
    public event EventHandler ClearEvent;
    public event EventHandler EolClearEvent;
    public event EventHandler<CursorGotoEventArgs> CursorGotoEvent;
    public event EventHandler<HighlightSetEventArgs> HighlightSetEvent;
    public event EventHandler<PutEventArgs> PutEvent;
    public event EventHandler<SetScrollRegionEventArgs> SetScrollRegionEvent;
    public event EventHandler<ScrollEventArgs> ScrollEvent;
    public event EventHandler<DefaultColorsSetEventArgs> DefaultColorsSetEvent;
    public event EventHandler<HlAttrDefineEventArgs> HlAttrDefineEvent;
    public event EventHandler<HlGroupSetEventArgs> HlGroupSetEvent;
    public event EventHandler<GridResizeEventArgs> GridResizeEvent;
    public event EventHandler<GridClearEventArgs> GridClearEvent;
    public event EventHandler<GridCursorGotoEventArgs> GridCursorGotoEvent;
    public event EventHandler<GridLineEventArgs> GridLineEvent;
    public event EventHandler<GridScrollEventArgs> GridScrollEvent;
    public event EventHandler<GridDestroyEventArgs> GridDestroyEvent;
    public event EventHandler<WinPosEventArgs> WinPosEvent;
    public event EventHandler<WinFloatPosEventArgs> WinFloatPosEvent;
    public event EventHandler<WinExternalPosEventArgs> WinExternalPosEvent;
    public event EventHandler<WinHideEventArgs> WinHideEvent;
    public event EventHandler<WinCloseEventArgs> WinCloseEvent;
    public event EventHandler<MsgSetPosEventArgs> MsgSetPosEvent;
    public event EventHandler<WinViewportEventArgs> WinViewportEvent;
    public event EventHandler<PopupmenuShowEventArgs> PopupmenuShowEvent;
    public event EventHandler PopupmenuHideEvent;
    public event EventHandler<PopupmenuSelectEventArgs> PopupmenuSelectEvent;
    public event EventHandler<TablineUpdateEventArgs> TablineUpdateEvent;
    public event EventHandler<CmdlineShowEventArgs> CmdlineShowEvent;
    public event EventHandler<CmdlinePosEventArgs> CmdlinePosEvent;
    public event EventHandler<CmdlineSpecialCharEventArgs> CmdlineSpecialCharEvent;
    public event EventHandler<CmdlineHideEventArgs> CmdlineHideEvent;
    public event EventHandler<CmdlineBlockShowEventArgs> CmdlineBlockShowEvent;
    public event EventHandler<CmdlineBlockAppendEventArgs> CmdlineBlockAppendEvent;
    public event EventHandler CmdlineBlockHideEvent;
    public event EventHandler<WildmenuShowEventArgs> WildmenuShowEvent;
    public event EventHandler<WildmenuSelectEventArgs> WildmenuSelectEvent;
    public event EventHandler WildmenuHideEvent;
    public event EventHandler<MsgShowEventArgs> MsgShowEvent;
    public event EventHandler MsgClearEvent;
    public event EventHandler<MsgShowcmdEventArgs> MsgShowcmdEvent;
    public event EventHandler<MsgShowmodeEventArgs> MsgShowmodeEvent;
    public event EventHandler<MsgRulerEventArgs> MsgRulerEvent;
    public event EventHandler<MsgHistoryShowEventArgs> MsgHistoryShowEvent;

    public Task<string> CommandOutput(string @command) =>
      SendAndReceive<string>(new NvimRequest
      {
        Method = "nvim_command_output",
        Arguments = new dynamic[] {
          @command
        }
      });

    public Task<dynamic> ExecuteLua(string @code, dynamic[] @args) =>
      SendAndReceive<dynamic>(new NvimRequest
      {
        Method = "nvim_execute_lua",
        Arguments = new dynamic[] {
          @code, @args
        }
      });

    public Task UiAttach(long @width, long @height, IDictionary @options) =>
      SendAndReceive(new NvimRequest
      {
        Method = "nvim_ui_attach",
        Arguments = new dynamic[] {
          @width, @height, @options
        }
      });

    public Task UiDetach() =>
      SendAndReceive(new NvimRequest
      {
        Method = "nvim_ui_detach",
        Arguments = new dynamic[] {
          
        }
      });

    public Task UiTryResize(long @width, long @height) =>
      SendAndReceive(new NvimRequest
      {
        Method = "nvim_ui_try_resize",
        Arguments = new dynamic[] {
          @width, @height
        }
      });

    public Task UiSetOption(string @name, dynamic @value) =>
      SendAndReceive(new NvimRequest
      {
        Method = "nvim_ui_set_option",
        Arguments = new dynamic[] {
          @name, @value
        }
      });

    public Task UiTryResizeGrid(long @grid, long @width, long @height) =>
      SendAndReceive(new NvimRequest
      {
        Method = "nvim_ui_try_resize_grid",
        Arguments = new dynamic[] {
          @grid, @width, @height
        }
      });

    public Task UiPumSetHeight(long @height) =>
      SendAndReceive(new NvimRequest
      {
        Method = "nvim_ui_pum_set_height",
        Arguments = new dynamic[] {
          @height
        }
      });

    public Task UiPumSetBounds(double @width, double @height, double @row, double @col) =>
      SendAndReceive(new NvimRequest
      {
        Method = "nvim_ui_pum_set_bounds",
        Arguments = new dynamic[] {
          @width, @height, @row, @col
        }
      });

    public Task<string> Exec(string @src, bool @output) =>
      SendAndReceive<string>(new NvimRequest
      {
        Method = "nvim_exec",
        Arguments = new dynamic[] {
          @src, @output
        }
      });

    public Task Command(string @command) =>
      SendAndReceive(new NvimRequest
      {
        Method = "nvim_command",
        Arguments = new dynamic[] {
          @command
        }
      });

    public Task<IDictionary> GetHlByName(string @name, bool @rgb) =>
      SendAndReceive<IDictionary>(new NvimRequest
      {
        Method = "nvim_get_hl_by_name",
        Arguments = new dynamic[] {
          @name, @rgb
        }
      });

    public Task<IDictionary> GetHlById(long @hlId, bool @rgb) =>
      SendAndReceive<IDictionary>(new NvimRequest
      {
        Method = "nvim_get_hl_by_id",
        Arguments = new dynamic[] {
          @hlId, @rgb
        }
      });

    public Task<long> GetHlIdByName(string @name) =>
      SendAndReceive<long>(new NvimRequest
      {
        Method = "nvim_get_hl_id_by_name",
        Arguments = new dynamic[] {
          @name
        }
      });

    public Task SetHl(long @nsId, string @name, IDictionary @val) =>
      SendAndReceive(new NvimRequest
      {
        Method = "nvim_set_hl",
        Arguments = new dynamic[] {
          @nsId, @name, @val
        }
      });

    public Task SetHlNs(long @nsId) =>
      SendAndReceive(new NvimRequest
      {
        Method = "nvim_set_hl_ns",
        Arguments = new dynamic[] {
          @nsId
        }
      });

    public Task Feedkeys(string @keys, string @mode, bool @escapeCsi) =>
      SendAndReceive(new NvimRequest
      {
        Method = "nvim_feedkeys",
        Arguments = new dynamic[] {
          @keys, @mode, @escapeCsi
        }
      });

    public Task<long> Input(string @keys) =>
      SendAndReceive<long>(new NvimRequest
      {
        Method = "nvim_input",
        Arguments = new dynamic[] {
          @keys
        }
      });

    public Task InputMouse(string @button, string @action, string @modifier, long @grid, long @row, long @col) =>
      SendAndReceive(new NvimRequest
      {
        Method = "nvim_input_mouse",
        Arguments = new dynamic[] {
          @button, @action, @modifier, @grid, @row, @col
        }
      });

    public Task<string> ReplaceTermcodes(string @str, bool @fromPart, bool @doLt, bool @special) =>
      SendAndReceive<string>(new NvimRequest
      {
        Method = "nvim_replace_termcodes",
        Arguments = new dynamic[] {
          @str, @fromPart, @doLt, @special
        }
      });

    public Task<dynamic> Eval(string @expr) =>
      SendAndReceive<dynamic>(new NvimRequest
      {
        Method = "nvim_eval",
        Arguments = new dynamic[] {
          @expr
        }
      });

    public Task<dynamic> ExecLua(string @code, dynamic[] @args) =>
      SendAndReceive<dynamic>(new NvimRequest
      {
        Method = "nvim_exec_lua",
        Arguments = new dynamic[] {
          @code, @args
        }
      });

    public Task<dynamic> CallFunction(string @fn, dynamic[] @args) =>
      SendAndReceive<dynamic>(new NvimRequest
      {
        Method = "nvim_call_function",
        Arguments = new dynamic[] {
          @fn, @args
        }
      });

    public Task<dynamic> CallDictFunction(dynamic @dict, string @fn, dynamic[] @args) =>
      SendAndReceive<dynamic>(new NvimRequest
      {
        Method = "nvim_call_dict_function",
        Arguments = new dynamic[] {
          @dict, @fn, @args
        }
      });

    public Task<long> Strwidth(string @text) =>
      SendAndReceive<long>(new NvimRequest
      {
        Method = "nvim_strwidth",
        Arguments = new dynamic[] {
          @text
        }
      });

    public Task<string[]> ListRuntimePaths() =>
      SendAndReceive<string[]>(new NvimRequest
      {
        Method = "nvim_list_runtime_paths",
        Arguments = new dynamic[] {
          
        }
      });

    public Task<string[]> GetRuntimeFile(string @name, bool @all) =>
      SendAndReceive<string[]>(new NvimRequest
      {
        Method = "nvim_get_runtime_file",
        Arguments = new dynamic[] {
          @name, @all
        }
      });

    public Task SetCurrentDir(string @dir) =>
      SendAndReceive(new NvimRequest
      {
        Method = "nvim_set_current_dir",
        Arguments = new dynamic[] {
          @dir
        }
      });

    public Task<string> GetCurrentLine() =>
      SendAndReceive<string>(new NvimRequest
      {
        Method = "nvim_get_current_line",
        Arguments = new dynamic[] {
          
        }
      });

    public Task SetCurrentLine(string @line) =>
      SendAndReceive(new NvimRequest
      {
        Method = "nvim_set_current_line",
        Arguments = new dynamic[] {
          @line
        }
      });

    public Task DelCurrentLine() =>
      SendAndReceive(new NvimRequest
      {
        Method = "nvim_del_current_line",
        Arguments = new dynamic[] {
          
        }
      });

    public Task<dynamic> GetVar(string @name) =>
      SendAndReceive<dynamic>(new NvimRequest
      {
        Method = "nvim_get_var",
        Arguments = new dynamic[] {
          @name
        }
      });

    public Task SetVar(string @name, dynamic @value) =>
      SendAndReceive(new NvimRequest
      {
        Method = "nvim_set_var",
        Arguments = new dynamic[] {
          @name, @value
        }
      });

    public Task DelVar(string @name) =>
      SendAndReceive(new NvimRequest
      {
        Method = "nvim_del_var",
        Arguments = new dynamic[] {
          @name
        }
      });

    public Task<dynamic> GetVvar(string @name) =>
      SendAndReceive<dynamic>(new NvimRequest
      {
        Method = "nvim_get_vvar",
        Arguments = new dynamic[] {
          @name
        }
      });

    public Task SetVvar(string @name, dynamic @value) =>
      SendAndReceive(new NvimRequest
      {
        Method = "nvim_set_vvar",
        Arguments = new dynamic[] {
          @name, @value
        }
      });

    public Task<dynamic> GetOption(string @name) =>
      SendAndReceive<dynamic>(new NvimRequest
      {
        Method = "nvim_get_option",
        Arguments = new dynamic[] {
          @name
        }
      });

    public Task<IDictionary> GetAllOptionsInfo() =>
      SendAndReceive<IDictionary>(new NvimRequest
      {
        Method = "nvim_get_all_options_info",
        Arguments = new dynamic[] {
          
        }
      });

    public Task<IDictionary> GetOptionInfo(string @name) =>
      SendAndReceive<IDictionary>(new NvimRequest
      {
        Method = "nvim_get_option_info",
        Arguments = new dynamic[] {
          @name
        }
      });

    public Task SetOption(string @name, dynamic @value) =>
      SendAndReceive(new NvimRequest
      {
        Method = "nvim_set_option",
        Arguments = new dynamic[] {
          @name, @value
        }
      });

    public Task OutWrite(string @str) =>
      SendAndReceive(new NvimRequest
      {
        Method = "nvim_out_write",
        Arguments = new dynamic[] {
          @str
        }
      });

    public Task ErrWrite(string @str) =>
      SendAndReceive(new NvimRequest
      {
        Method = "nvim_err_write",
        Arguments = new dynamic[] {
          @str
        }
      });

    public Task ErrWriteln(string @str) =>
      SendAndReceive(new NvimRequest
      {
        Method = "nvim_err_writeln",
        Arguments = new dynamic[] {
          @str
        }
      });

    public Task<NvimBuffer[]> ListBufs() =>
      SendAndReceive<NvimBuffer[]>(new NvimRequest
      {
        Method = "nvim_list_bufs",
        Arguments = new dynamic[] {
          
        }
      });

    public Task<NvimBuffer> GetCurrentBuf() =>
      SendAndReceive<NvimBuffer>(new NvimRequest
      {
        Method = "nvim_get_current_buf",
        Arguments = new dynamic[] {
          
        }
      });

    public Task SetCurrentBuf(NvimBuffer @buffer) =>
      SendAndReceive(new NvimRequest
      {
        Method = "nvim_set_current_buf",
        Arguments = new dynamic[] {
          @buffer
        }
      });

    public Task<NvimWindow[]> ListWins() =>
      SendAndReceive<NvimWindow[]>(new NvimRequest
      {
        Method = "nvim_list_wins",
        Arguments = new dynamic[] {
          
        }
      });

    public Task<NvimWindow> GetCurrentWin() =>
      SendAndReceive<NvimWindow>(new NvimRequest
      {
        Method = "nvim_get_current_win",
        Arguments = new dynamic[] {
          
        }
      });

    public Task SetCurrentWin(NvimWindow @window) =>
      SendAndReceive(new NvimRequest
      {
        Method = "nvim_set_current_win",
        Arguments = new dynamic[] {
          @window
        }
      });

    public Task<NvimBuffer> CreateBuf(bool @listed, bool @scratch) =>
      SendAndReceive<NvimBuffer>(new NvimRequest
      {
        Method = "nvim_create_buf",
        Arguments = new dynamic[] {
          @listed, @scratch
        }
      });

    public Task<NvimWindow> OpenWin(NvimBuffer @buffer, bool @enter, IDictionary @config) =>
      SendAndReceive<NvimWindow>(new NvimRequest
      {
        Method = "nvim_open_win",
        Arguments = new dynamic[] {
          @buffer, @enter, @config
        }
      });

    public Task<NvimTabpage[]> ListTabpages() =>
      SendAndReceive<NvimTabpage[]>(new NvimRequest
      {
        Method = "nvim_list_tabpages",
        Arguments = new dynamic[] {
          
        }
      });

    public Task<NvimTabpage> GetCurrentTabpage() =>
      SendAndReceive<NvimTabpage>(new NvimRequest
      {
        Method = "nvim_get_current_tabpage",
        Arguments = new dynamic[] {
          
        }
      });

    public Task SetCurrentTabpage(NvimTabpage @tabpage) =>
      SendAndReceive(new NvimRequest
      {
        Method = "nvim_set_current_tabpage",
        Arguments = new dynamic[] {
          @tabpage
        }
      });

    public Task<long> CreateNamespace(string @name) =>
      SendAndReceive<long>(new NvimRequest
      {
        Method = "nvim_create_namespace",
        Arguments = new dynamic[] {
          @name
        }
      });

    public Task<IDictionary> GetNamespaces() =>
      SendAndReceive<IDictionary>(new NvimRequest
      {
        Method = "nvim_get_namespaces",
        Arguments = new dynamic[] {
          
        }
      });

    public Task<bool> Paste(string @data, bool @crlf, long @phase) =>
      SendAndReceive<bool>(new NvimRequest
      {
        Method = "nvim_paste",
        Arguments = new dynamic[] {
          @data, @crlf, @phase
        }
      });

    public Task Put(string[] @lines, string @type, bool @after, bool @follow) =>
      SendAndReceive(new NvimRequest
      {
        Method = "nvim_put",
        Arguments = new dynamic[] {
          @lines, @type, @after, @follow
        }
      });

    public Task Subscribe(string @event) =>
      SendAndReceive(new NvimRequest
      {
        Method = "nvim_subscribe",
        Arguments = new dynamic[] {
          @event
        }
      });

    public Task Unsubscribe(string @event) =>
      SendAndReceive(new NvimRequest
      {
        Method = "nvim_unsubscribe",
        Arguments = new dynamic[] {
          @event
        }
      });

    public Task<long> GetColorByName(string @name) =>
      SendAndReceive<long>(new NvimRequest
      {
        Method = "nvim_get_color_by_name",
        Arguments = new dynamic[] {
          @name
        }
      });

    public Task<IDictionary> GetColorMap() =>
      SendAndReceive<IDictionary>(new NvimRequest
      {
        Method = "nvim_get_color_map",
        Arguments = new dynamic[] {
          
        }
      });

    public Task<IDictionary> GetContext(IDictionary @opts) =>
      SendAndReceive<IDictionary>(new NvimRequest
      {
        Method = "nvim_get_context",
        Arguments = new dynamic[] {
          @opts
        }
      });

    public Task<dynamic> LoadContext(IDictionary @dict) =>
      SendAndReceive<dynamic>(new NvimRequest
      {
        Method = "nvim_load_context",
        Arguments = new dynamic[] {
          @dict
        }
      });

    public Task<IDictionary> GetMode() =>
      SendAndReceive<IDictionary>(new NvimRequest
      {
        Method = "nvim_get_mode",
        Arguments = new dynamic[] {
          
        }
      });

    public Task<IDictionary[]> GetKeymap(string @mode) =>
      SendAndReceive<IDictionary[]>(new NvimRequest
      {
        Method = "nvim_get_keymap",
        Arguments = new dynamic[] {
          @mode
        }
      });

    public Task SetKeymap(string @mode, string @lhs, string @rhs, IDictionary @opts) =>
      SendAndReceive(new NvimRequest
      {
        Method = "nvim_set_keymap",
        Arguments = new dynamic[] {
          @mode, @lhs, @rhs, @opts
        }
      });

    public Task DelKeymap(string @mode, string @lhs) =>
      SendAndReceive(new NvimRequest
      {
        Method = "nvim_del_keymap",
        Arguments = new dynamic[] {
          @mode, @lhs
        }
      });

    public Task<IDictionary> GetCommands(IDictionary @opts) =>
      SendAndReceive<IDictionary>(new NvimRequest
      {
        Method = "nvim_get_commands",
        Arguments = new dynamic[] {
          @opts
        }
      });

    public Task<dynamic[]> GetApiInfo() =>
      SendAndReceive<dynamic[]>(new NvimRequest
      {
        Method = "nvim_get_api_info",
        Arguments = new dynamic[] {
          
        }
      });

    public Task SetClientInfo(string @name, IDictionary @version, string @type, IDictionary @methods, IDictionary @attributes) =>
      SendAndReceive(new NvimRequest
      {
        Method = "nvim_set_client_info",
        Arguments = new dynamic[] {
          @name, @version, @type, @methods, @attributes
        }
      });

    public Task<IDictionary> GetChanInfo(long @chan) =>
      SendAndReceive<IDictionary>(new NvimRequest
      {
        Method = "nvim_get_chan_info",
        Arguments = new dynamic[] {
          @chan
        }
      });

    public Task<dynamic[]> ListChans() =>
      SendAndReceive<dynamic[]>(new NvimRequest
      {
        Method = "nvim_list_chans",
        Arguments = new dynamic[] {
          
        }
      });

    public Task<dynamic[]> CallAtomic(dynamic[] @calls) =>
      SendAndReceive<dynamic[]>(new NvimRequest
      {
        Method = "nvim_call_atomic",
        Arguments = new dynamic[] {
          @calls
        }
      });

    public Task<IDictionary> ParseExpression(string @expr, string @flags, bool @highlight) =>
      SendAndReceive<IDictionary>(new NvimRequest
      {
        Method = "nvim_parse_expression",
        Arguments = new dynamic[] {
          @expr, @flags, @highlight
        }
      });

    public Task<dynamic[]> ListUis() =>
      SendAndReceive<dynamic[]>(new NvimRequest
      {
        Method = "nvim_list_uis",
        Arguments = new dynamic[] {
          
        }
      });

    public Task<dynamic[]> GetProcChildren(long @pid) =>
      SendAndReceive<dynamic[]>(new NvimRequest
      {
        Method = "nvim_get_proc_children",
        Arguments = new dynamic[] {
          @pid
        }
      });

    public Task<dynamic> GetProc(long @pid) =>
      SendAndReceive<dynamic>(new NvimRequest
      {
        Method = "nvim_get_proc",
        Arguments = new dynamic[] {
          @pid
        }
      });

    public Task SelectPopupmenuItem(long @item, bool @insert, bool @finish, IDictionary @opts) =>
      SendAndReceive(new NvimRequest
      {
        Method = "nvim_select_popupmenu_item",
        Arguments = new dynamic[] {
          @item, @insert, @finish, @opts
        }
      });

    public Task SetDecorationProvider(long @nsId, IDictionary @opts) =>
      SendAndReceive(new NvimRequest
      {
        Method = "nvim_set_decoration_provider",
        Arguments = new dynamic[] {
          @nsId, @opts
        }
      });


  public class NvimBuffer
  {
    private readonly NvimAPI _api;
    //private readonly MessagePackExtendedTypeObject _msgPackExtObj;
    internal NvimBuffer(NvimAPI api/*, MessagePackExtendedTypeObject msgPackExtObj*/)
    {
      _api = api;
      //_msgPackExtObj = msgPackExtObj;
    }
    
    public Task<long> LineCount() =>
      _api.SendAndReceive<long>(new NvimRequest
      {
        Method = "nvim_buf_line_count",
        Arguments = new dynamic[] {
          
        }
      });

    public Task<bool> Attach(bool @sendBuffer, IDictionary @opts) =>
      _api.SendAndReceive<bool>(new NvimRequest
      {
        Method = "nvim_buf_attach",
        Arguments = new dynamic[] {
          @sendBuffer, @opts
        }
      });

    public Task<bool> Detach() =>
      _api.SendAndReceive<bool>(new NvimRequest
      {
        Method = "nvim_buf_detach",
        Arguments = new dynamic[] {
          
        }
      });

    public Task<string[]> GetLines(long @start, long @end, bool @strictIndexing) =>
      _api.SendAndReceive<string[]>(new NvimRequest
      {
        Method = "nvim_buf_get_lines",
        Arguments = new dynamic[] {
          @start, @end, @strictIndexing
        }
      });

    public Task SetLines(long @start, long @end, bool @strictIndexing, string[] @replacement) =>
      _api.SendAndReceive(new NvimRequest
      {
        Method = "nvim_buf_set_lines",
        Arguments = new dynamic[] {
          @start, @end, @strictIndexing, @replacement
        }
      });

    public Task<long> GetOffset(long @index) =>
      _api.SendAndReceive<long>(new NvimRequest
      {
        Method = "nvim_buf_get_offset",
        Arguments = new dynamic[] {
          @index
        }
      });

    public Task<dynamic> GetVar(string @name) =>
      _api.SendAndReceive<dynamic>(new NvimRequest
      {
        Method = "nvim_buf_get_var",
        Arguments = new dynamic[] {
          @name
        }
      });

    public Task<long> GetChangedtick() =>
      _api.SendAndReceive<long>(new NvimRequest
      {
        Method = "nvim_buf_get_changedtick",
        Arguments = new dynamic[] {
          
        }
      });

    public Task<IDictionary[]> GetKeymap(string @mode) =>
      _api.SendAndReceive<IDictionary[]>(new NvimRequest
      {
        Method = "nvim_buf_get_keymap",
        Arguments = new dynamic[] {
          @mode
        }
      });

    public Task SetKeymap(string @mode, string @lhs, string @rhs, IDictionary @opts) =>
      _api.SendAndReceive(new NvimRequest
      {
        Method = "nvim_buf_set_keymap",
        Arguments = new dynamic[] {
          @mode, @lhs, @rhs, @opts
        }
      });

    public Task DelKeymap(string @mode, string @lhs) =>
      _api.SendAndReceive(new NvimRequest
      {
        Method = "nvim_buf_del_keymap",
        Arguments = new dynamic[] {
          @mode, @lhs
        }
      });

    public Task<IDictionary> GetCommands(IDictionary @opts) =>
      _api.SendAndReceive<IDictionary>(new NvimRequest
      {
        Method = "nvim_buf_get_commands",
        Arguments = new dynamic[] {
          @opts
        }
      });

    public Task SetVar(string @name, dynamic @value) =>
      _api.SendAndReceive(new NvimRequest
      {
        Method = "nvim_buf_set_var",
        Arguments = new dynamic[] {
          @name, @value
        }
      });

    public Task DelVar(string @name) =>
      _api.SendAndReceive(new NvimRequest
      {
        Method = "nvim_buf_del_var",
        Arguments = new dynamic[] {
          @name
        }
      });

    public Task<dynamic> GetOption(string @name) =>
      _api.SendAndReceive<dynamic>(new NvimRequest
      {
        Method = "nvim_buf_get_option",
        Arguments = new dynamic[] {
          @name
        }
      });

    public Task SetOption(string @name, dynamic @value) =>
      _api.SendAndReceive(new NvimRequest
      {
        Method = "nvim_buf_set_option",
        Arguments = new dynamic[] {
          @name, @value
        }
      });

    public Task<string> GetName() =>
      _api.SendAndReceive<string>(new NvimRequest
      {
        Method = "nvim_buf_get_name",
        Arguments = new dynamic[] {
          
        }
      });

    public Task SetName(string @name) =>
      _api.SendAndReceive(new NvimRequest
      {
        Method = "nvim_buf_set_name",
        Arguments = new dynamic[] {
          @name
        }
      });

    public Task<bool> IsLoaded() =>
      _api.SendAndReceive<bool>(new NvimRequest
      {
        Method = "nvim_buf_is_loaded",
        Arguments = new dynamic[] {
          
        }
      });

    public Task Delete(IDictionary @opts) =>
      _api.SendAndReceive(new NvimRequest
      {
        Method = "nvim_buf_delete",
        Arguments = new dynamic[] {
          @opts
        }
      });

    public Task<bool> IsValid() =>
      _api.SendAndReceive<bool>(new NvimRequest
      {
        Method = "nvim_buf_is_valid",
        Arguments = new dynamic[] {
          
        }
      });

    public Task<long[]> GetMark(string @name) =>
      _api.SendAndReceive<long[]>(new NvimRequest
      {
        Method = "nvim_buf_get_mark",
        Arguments = new dynamic[] {
          @name
        }
      });

    public Task<long[]> GetExtmarkById(long @nsId, long @id, IDictionary @opts) =>
      _api.SendAndReceive<long[]>(new NvimRequest
      {
        Method = "nvim_buf_get_extmark_by_id",
        Arguments = new dynamic[] {
          @nsId, @id, @opts
        }
      });

    public Task<dynamic[]> GetExtmarks(long @nsId, dynamic @start, dynamic @end, IDictionary @opts) =>
      _api.SendAndReceive<dynamic[]>(new NvimRequest
      {
        Method = "nvim_buf_get_extmarks",
        Arguments = new dynamic[] {
          @nsId, @start, @end, @opts
        }
      });

    public Task<long> SetExtmark(long @nsId, long @line, long @col, IDictionary @opts) =>
      _api.SendAndReceive<long>(new NvimRequest
      {
        Method = "nvim_buf_set_extmark",
        Arguments = new dynamic[] {
          @nsId, @line, @col, @opts
        }
      });

    public Task<bool> DelExtmark(long @nsId, long @id) =>
      _api.SendAndReceive<bool>(new NvimRequest
      {
        Method = "nvim_buf_del_extmark",
        Arguments = new dynamic[] {
          @nsId, @id
        }
      });

    public Task<long> AddHighlight(long @srcId, string @hlGroup, long @line, long @colStart, long @colEnd) =>
      _api.SendAndReceive<long>(new NvimRequest
      {
        Method = "nvim_buf_add_highlight",
        Arguments = new dynamic[] {
          @srcId, @hlGroup, @line, @colStart, @colEnd
        }
      });

    public Task ClearNamespace(long @nsId, long @lineStart, long @lineEnd) =>
      _api.SendAndReceive(new NvimRequest
      {
        Method = "nvim_buf_clear_namespace",
        Arguments = new dynamic[] {
          @nsId, @lineStart, @lineEnd
        }
      });

    public Task<long> SetVirtualText(long @srcId, long @line, dynamic[] @chunks, IDictionary @opts) =>
      _api.SendAndReceive<long>(new NvimRequest
      {
        Method = "nvim_buf_set_virtual_text",
        Arguments = new dynamic[] {
          @srcId, @line, @chunks, @opts
        }
      });

    public Task ClearHighlight(long @nsId, long @lineStart, long @lineEnd) =>
      _api.SendAndReceive(new NvimRequest
      {
        Method = "nvim_buf_clear_highlight",
        Arguments = new dynamic[] {
          @nsId, @lineStart, @lineEnd
        }
      });

  }
  public class NvimWindow
  {
    private readonly NvimAPI _api;
    //private readonly MessagePackExtendedTypeObject _msgPackExtObj;
    internal NvimWindow(NvimAPI api/*, MessagePackExtendedTypeObject msgPackExtObj*/)
    {
      _api = api;
      //_msgPackExtObj = msgPackExtObj;
    }
    
    public Task<NvimBuffer> GetBuf() =>
      _api.SendAndReceive<NvimBuffer>(new NvimRequest
      {
        Method = "nvim_win_get_buf",
        Arguments = new dynamic[] {
          
        }
      });

    public Task SetBuf(NvimBuffer @buffer) =>
      _api.SendAndReceive(new NvimRequest
      {
        Method = "nvim_win_set_buf",
        Arguments = new dynamic[] {
          @buffer
        }
      });

    public Task<long[]> GetCursor() =>
      _api.SendAndReceive<long[]>(new NvimRequest
      {
        Method = "nvim_win_get_cursor",
        Arguments = new dynamic[] {
          
        }
      });

    public Task SetCursor(long[] @pos) =>
      _api.SendAndReceive(new NvimRequest
      {
        Method = "nvim_win_set_cursor",
        Arguments = new dynamic[] {
          @pos
        }
      });

    public Task<long> GetHeight() =>
      _api.SendAndReceive<long>(new NvimRequest
      {
        Method = "nvim_win_get_height",
        Arguments = new dynamic[] {
          
        }
      });

    public Task SetHeight(long @height) =>
      _api.SendAndReceive(new NvimRequest
      {
        Method = "nvim_win_set_height",
        Arguments = new dynamic[] {
          @height
        }
      });

    public Task<long> GetWidth() =>
      _api.SendAndReceive<long>(new NvimRequest
      {
        Method = "nvim_win_get_width",
        Arguments = new dynamic[] {
          
        }
      });

    public Task SetWidth(long @width) =>
      _api.SendAndReceive(new NvimRequest
      {
        Method = "nvim_win_set_width",
        Arguments = new dynamic[] {
          @width
        }
      });

    public Task<dynamic> GetVar(string @name) =>
      _api.SendAndReceive<dynamic>(new NvimRequest
      {
        Method = "nvim_win_get_var",
        Arguments = new dynamic[] {
          @name
        }
      });

    public Task SetVar(string @name, dynamic @value) =>
      _api.SendAndReceive(new NvimRequest
      {
        Method = "nvim_win_set_var",
        Arguments = new dynamic[] {
          @name, @value
        }
      });

    public Task DelVar(string @name) =>
      _api.SendAndReceive(new NvimRequest
      {
        Method = "nvim_win_del_var",
        Arguments = new dynamic[] {
          @name
        }
      });

    public Task<dynamic> GetOption(string @name) =>
      _api.SendAndReceive<dynamic>(new NvimRequest
      {
        Method = "nvim_win_get_option",
        Arguments = new dynamic[] {
          @name
        }
      });

    public Task SetOption(string @name, dynamic @value) =>
      _api.SendAndReceive(new NvimRequest
      {
        Method = "nvim_win_set_option",
        Arguments = new dynamic[] {
          @name, @value
        }
      });

    public Task<long[]> GetPosition() =>
      _api.SendAndReceive<long[]>(new NvimRequest
      {
        Method = "nvim_win_get_position",
        Arguments = new dynamic[] {
          
        }
      });

    public Task<NvimTabpage> GetTabpage() =>
      _api.SendAndReceive<NvimTabpage>(new NvimRequest
      {
        Method = "nvim_win_get_tabpage",
        Arguments = new dynamic[] {
          
        }
      });

    public Task<long> GetNumber() =>
      _api.SendAndReceive<long>(new NvimRequest
      {
        Method = "nvim_win_get_number",
        Arguments = new dynamic[] {
          
        }
      });

    public Task<bool> IsValid() =>
      _api.SendAndReceive<bool>(new NvimRequest
      {
        Method = "nvim_win_is_valid",
        Arguments = new dynamic[] {
          
        }
      });

    public Task SetConfig(IDictionary @config) =>
      _api.SendAndReceive(new NvimRequest
      {
        Method = "nvim_win_set_config",
        Arguments = new dynamic[] {
          @config
        }
      });

    public Task<IDictionary> GetConfig() =>
      _api.SendAndReceive<IDictionary>(new NvimRequest
      {
        Method = "nvim_win_get_config",
        Arguments = new dynamic[] {
          
        }
      });

    public Task Close(bool @force) =>
      _api.SendAndReceive(new NvimRequest
      {
        Method = "nvim_win_close",
        Arguments = new dynamic[] {
          @force
        }
      });

  }
  public class NvimTabpage
  {
    private readonly NvimAPI _api;
    //private readonly MessagePackExtendedTypeObject _msgPackExtObj;
    internal NvimTabpage(NvimAPI api/*, MessagePackExtendedTypeObject msgPackExtObj*/)
    {
      _api = api;
      //_msgPackExtObj = msgPackExtObj;
    }
    
    public Task<NvimWindow[]> ListWins() =>
      _api.SendAndReceive<NvimWindow[]>(new NvimRequest
      {
        Method = "nvim_tabpage_list_wins",
        Arguments = new dynamic[] {
          
        }
      });

    public Task<dynamic> GetVar(string @name) =>
      _api.SendAndReceive<dynamic>(new NvimRequest
      {
        Method = "nvim_tabpage_get_var",
        Arguments = new dynamic[] {
          @name
        }
      });

    public Task SetVar(string @name, dynamic @value) =>
      _api.SendAndReceive(new NvimRequest
      {
        Method = "nvim_tabpage_set_var",
        Arguments = new dynamic[] {
          @name, @value
        }
      });

    public Task DelVar(string @name) =>
      _api.SendAndReceive(new NvimRequest
      {
        Method = "nvim_tabpage_del_var",
        Arguments = new dynamic[] {
          @name
        }
      });

    public Task<NvimWindow> GetWin() =>
      _api.SendAndReceive<NvimWindow>(new NvimRequest
      {
        Method = "nvim_tabpage_get_win",
        Arguments = new dynamic[] {
          
        }
      });

    public Task<long> GetNumber() =>
      _api.SendAndReceive<long>(new NvimRequest
      {
        Method = "nvim_tabpage_get_number",
        Arguments = new dynamic[] {
          
        }
      });

    public Task<bool> IsValid() =>
      _api.SendAndReceive<bool>(new NvimRequest
      {
        Method = "nvim_tabpage_is_valid",
        Arguments = new dynamic[] {
          
        }
      });

  }

  public class ModeInfoSetEventArgs : EventArgs
  {
    public bool Enabled { get; set; }
    public dynamic[] CursorStyles { get; set; }

  }
  public class ModeChangeEventArgs : EventArgs
  {
    public string Mode { get; set; }
    public long ModeIdx { get; set; }

  }
  public class SetTitleEventArgs : EventArgs
  {
    public string Title { get; set; }

  }
  public class SetIconEventArgs : EventArgs
  {
    public string Icon { get; set; }

  }
  public class ScreenshotEventArgs : EventArgs
  {
    public string Path { get; set; }

  }
  public class OptionSetEventArgs : EventArgs
  {
    public string Name { get; set; }
    public dynamic Value { get; set; }

  }
  public class UpdateFgEventArgs : EventArgs
  {
    public long Fg { get; set; }

  }
  public class UpdateBgEventArgs : EventArgs
  {
    public long Bg { get; set; }

  }
  public class UpdateSpEventArgs : EventArgs
  {
    public long Sp { get; set; }

  }
  public class ResizeEventArgs : EventArgs
  {
    public long Width { get; set; }
    public long Height { get; set; }

  }
  public class CursorGotoEventArgs : EventArgs
  {
    public long Row { get; set; }
    public long Col { get; set; }

  }
  public class HighlightSetEventArgs : EventArgs
  {
    public IDictionary Attrs { get; set; }

  }
  public class PutEventArgs : EventArgs
  {
    public string Str { get; set; }

  }
  public class SetScrollRegionEventArgs : EventArgs
  {
    public long Top { get; set; }
    public long Bot { get; set; }
    public long Left { get; set; }
    public long Right { get; set; }

  }
  public class ScrollEventArgs : EventArgs
  {
    public long Count { get; set; }

  }
  public class DefaultColorsSetEventArgs : EventArgs
  {
    public long RgbFg { get; set; }
    public long RgbBg { get; set; }
    public long RgbSp { get; set; }
    public long CtermFg { get; set; }
    public long CtermBg { get; set; }

  }
  public class HlAttrDefineEventArgs : EventArgs
  {
    public long Id { get; set; }
    public IDictionary RgbAttrs { get; set; }
    public IDictionary CtermAttrs { get; set; }
    public dynamic[] Info { get; set; }

  }
  public class HlGroupSetEventArgs : EventArgs
  {
    public string Name { get; set; }
    public long Id { get; set; }

  }
  public class GridResizeEventArgs : EventArgs
  {
    public long Grid { get; set; }
    public long Width { get; set; }
    public long Height { get; set; }

  }
  public class GridClearEventArgs : EventArgs
  {
    public long Grid { get; set; }

  }
  public class GridCursorGotoEventArgs : EventArgs
  {
    public long Grid { get; set; }
    public long Row { get; set; }
    public long Col { get; set; }

  }
  public class GridLineEventArgs : EventArgs
  {
    public long Grid { get; set; }
    public long Row { get; set; }
    public long ColStart { get; set; }
    public dynamic[] Data { get; set; }

  }
  public class GridScrollEventArgs : EventArgs
  {
    public long Grid { get; set; }
    public long Top { get; set; }
    public long Bot { get; set; }
    public long Left { get; set; }
    public long Right { get; set; }
    public long Rows { get; set; }
    public long Cols { get; set; }

  }
  public class GridDestroyEventArgs : EventArgs
  {
    public long Grid { get; set; }

  }
  public class WinPosEventArgs : EventArgs
  {
    public long Grid { get; set; }
    public NvimWindow Win { get; set; }
    public long Startrow { get; set; }
    public long Startcol { get; set; }
    public long Width { get; set; }
    public long Height { get; set; }

  }
  public class WinFloatPosEventArgs : EventArgs
  {
    public long Grid { get; set; }
    public NvimWindow Win { get; set; }
    public string Anchor { get; set; }
    public long AnchorGrid { get; set; }
    public double AnchorRow { get; set; }
    public double AnchorCol { get; set; }
    public bool Focusable { get; set; }

  }
  public class WinExternalPosEventArgs : EventArgs
  {
    public long Grid { get; set; }
    public NvimWindow Win { get; set; }

  }
  public class WinHideEventArgs : EventArgs
  {
    public long Grid { get; set; }

  }
  public class WinCloseEventArgs : EventArgs
  {
    public long Grid { get; set; }

  }
  public class MsgSetPosEventArgs : EventArgs
  {
    public long Grid { get; set; }
    public long Row { get; set; }
    public bool Scrolled { get; set; }
    public string SepChar { get; set; }

  }
  public class WinViewportEventArgs : EventArgs
  {
    public long Grid { get; set; }
    public NvimWindow Win { get; set; }
    public long Topline { get; set; }
    public long Botline { get; set; }
    public long Curline { get; set; }
    public long Curcol { get; set; }

  }
  public class PopupmenuShowEventArgs : EventArgs
  {
    public dynamic[] Items { get; set; }
    public long Selected { get; set; }
    public long Row { get; set; }
    public long Col { get; set; }
    public long Grid { get; set; }

  }
  public class PopupmenuSelectEventArgs : EventArgs
  {
    public long Selected { get; set; }

  }
  public class TablineUpdateEventArgs : EventArgs
  {
    public NvimTabpage Current { get; set; }
    public dynamic[] Tabs { get; set; }

  }
  public class CmdlineShowEventArgs : EventArgs
  {
    public dynamic[] Content { get; set; }
    public long Pos { get; set; }
    public string Firstc { get; set; }
    public string Prompt { get; set; }
    public long Indent { get; set; }
    public long Level { get; set; }

  }
  public class CmdlinePosEventArgs : EventArgs
  {
    public long Pos { get; set; }
    public long Level { get; set; }

  }
  public class CmdlineSpecialCharEventArgs : EventArgs
  {
    public string C { get; set; }
    public bool Shift { get; set; }
    public long Level { get; set; }

  }
  public class CmdlineHideEventArgs : EventArgs
  {
    public long Level { get; set; }

  }
  public class CmdlineBlockShowEventArgs : EventArgs
  {
    public dynamic[] Lines { get; set; }

  }
  public class CmdlineBlockAppendEventArgs : EventArgs
  {
    public dynamic[] Lines { get; set; }

  }
  public class WildmenuShowEventArgs : EventArgs
  {
    public dynamic[] Items { get; set; }

  }
  public class WildmenuSelectEventArgs : EventArgs
  {
    public long Selected { get; set; }

  }
  public class MsgShowEventArgs : EventArgs
  {
    public string Kind { get; set; }
    public dynamic[] Content { get; set; }
    public bool ReplaceLast { get; set; }

  }
  public class MsgShowcmdEventArgs : EventArgs
  {
    public dynamic[] Content { get; set; }

  }
  public class MsgShowmodeEventArgs : EventArgs
  {
    public dynamic[] Content { get; set; }

  }
  public class MsgRulerEventArgs : EventArgs
  {
    public dynamic[] Content { get; set; }

  }
  public class MsgHistoryShowEventArgs : EventArgs
  {
    public dynamic[] Entries { get; set; }

  }
    private void CallUIEventHandler(string eventName, object[] args)
    {
      switch (eventName)
      {
  
      case "mode_info_set":
          ModeInfoSetEvent?.Invoke(this, new ModeInfoSetEventArgs
          {
            Enabled = (bool) args[0],
            CursorStyles = (dynamic[]) args[1]
          });
          break;

      case "update_menu":
          UpdateMenuEvent?.Invoke(this, EventArgs.Empty);
          break;

      case "busy_start":
          BusyStartEvent?.Invoke(this, EventArgs.Empty);
          break;

      case "busy_stop":
          BusyStopEvent?.Invoke(this, EventArgs.Empty);
          break;

      case "mouse_on":
          MouseOnEvent?.Invoke(this, EventArgs.Empty);
          break;

      case "mouse_off":
          MouseOffEvent?.Invoke(this, EventArgs.Empty);
          break;

      case "mode_change":
          ModeChangeEvent?.Invoke(this, new ModeChangeEventArgs
          {
            Mode = (string) args[0],
            ModeIdx = (long) args[1]
          });
          break;

      case "bell":
          BellEvent?.Invoke(this, EventArgs.Empty);
          break;

      case "visual_bell":
          VisualBellEvent?.Invoke(this, EventArgs.Empty);
          break;

      case "flush":
          FlushEvent?.Invoke(this, EventArgs.Empty);
          break;

      case "suspend":
          SuspendEvent?.Invoke(this, EventArgs.Empty);
          break;

      case "set_title":
          SetTitleEvent?.Invoke(this, new SetTitleEventArgs
          {
            Title = (string) args[0]
          });
          break;

      case "set_icon":
          SetIconEvent?.Invoke(this, new SetIconEventArgs
          {
            Icon = (string) args[0]
          });
          break;

      case "screenshot":
          ScreenshotEvent?.Invoke(this, new ScreenshotEventArgs
          {
            Path = (string) args[0]
          });
          break;

      case "option_set":
          OptionSetEvent?.Invoke(this, new OptionSetEventArgs
          {
            Name = (string) args[0],
            Value = (dynamic) args[1]
          });
          break;

      case "update_fg":
          UpdateFgEvent?.Invoke(this, new UpdateFgEventArgs
          {
            Fg = (long) args[0]
          });
          break;

      case "update_bg":
          UpdateBgEvent?.Invoke(this, new UpdateBgEventArgs
          {
            Bg = (long) args[0]
          });
          break;

      case "update_sp":
          UpdateSpEvent?.Invoke(this, new UpdateSpEventArgs
          {
            Sp = (long) args[0]
          });
          break;

      case "resize":
          ResizeEvent?.Invoke(this, new ResizeEventArgs
          {
            Width = (long) args[0],
            Height = (long) args[1]
          });
          break;

      case "clear":
          ClearEvent?.Invoke(this, EventArgs.Empty);
          break;

      case "eol_clear":
          EolClearEvent?.Invoke(this, EventArgs.Empty);
          break;

      case "cursor_goto":
          CursorGotoEvent?.Invoke(this, new CursorGotoEventArgs
          {
            Row = (long) args[0],
            Col = (long) args[1]
          });
          break;

      case "highlight_set":
          HighlightSetEvent?.Invoke(this, new HighlightSetEventArgs
          {
            Attrs = (IDictionary) args[0]
          });
          break;

      case "put":
          PutEvent?.Invoke(this, new PutEventArgs
          {
            Str = (string) args[0]
          });
          break;

      case "set_scroll_region":
          SetScrollRegionEvent?.Invoke(this, new SetScrollRegionEventArgs
          {
            Top = (long) args[0],
            Bot = (long) args[1],
            Left = (long) args[2],
            Right = (long) args[3]
          });
          break;

      case "scroll":
          ScrollEvent?.Invoke(this, new ScrollEventArgs
          {
            Count = (long) args[0]
          });
          break;

      case "default_colors_set":
          DefaultColorsSetEvent?.Invoke(this, new DefaultColorsSetEventArgs
          {
            RgbFg = (long) args[0],
            RgbBg = (long) args[1],
            RgbSp = (long) args[2],
            CtermFg = (long) args[3],
            CtermBg = (long) args[4]
          });
          break;

      case "hl_attr_define":
          HlAttrDefineEvent?.Invoke(this, new HlAttrDefineEventArgs
          {
            Id = (long) args[0],
            RgbAttrs = (IDictionary) args[1],
            CtermAttrs = (IDictionary) args[2],
            Info = (dynamic[]) args[3]
          });
          break;

      case "hl_group_set":
          HlGroupSetEvent?.Invoke(this, new HlGroupSetEventArgs
          {
            Name = (string) args[0],
            Id = (long) args[1]
          });
          break;

      case "grid_resize":
          GridResizeEvent?.Invoke(this, new GridResizeEventArgs
          {
            Grid = (long) args[0],
            Width = (long) args[1],
            Height = (long) args[2]
          });
          break;

      case "grid_clear":
          GridClearEvent?.Invoke(this, new GridClearEventArgs
          {
            Grid = (long) args[0]
          });
          break;

      case "grid_cursor_goto":
          GridCursorGotoEvent?.Invoke(this, new GridCursorGotoEventArgs
          {
            Grid = (long) args[0],
            Row = (long) args[1],
            Col = (long) args[2]
          });
          break;

      case "grid_line":
          GridLineEvent?.Invoke(this, new GridLineEventArgs
          {
            Grid = (long) args[0],
            Row = (long) args[1],
            ColStart = (long) args[2],
            Data = (dynamic[]) args[3]
          });
          break;

      case "grid_scroll":
          GridScrollEvent?.Invoke(this, new GridScrollEventArgs
          {
            Grid = (long) args[0],
            Top = (long) args[1],
            Bot = (long) args[2],
            Left = (long) args[3],
            Right = (long) args[4],
            Rows = (long) args[5],
            Cols = (long) args[6]
          });
          break;

      case "grid_destroy":
          GridDestroyEvent?.Invoke(this, new GridDestroyEventArgs
          {
            Grid = (long) args[0]
          });
          break;

      case "win_pos":
          WinPosEvent?.Invoke(this, new WinPosEventArgs
          {
            Grid = (long) args[0],
            Win = (NvimWindow) args[1],
            Startrow = (long) args[2],
            Startcol = (long) args[3],
            Width = (long) args[4],
            Height = (long) args[5]
          });
          break;

      case "win_float_pos":
          WinFloatPosEvent?.Invoke(this, new WinFloatPosEventArgs
          {
            Grid = (long) args[0],
            Win = (NvimWindow) args[1],
            Anchor = (string) args[2],
            AnchorGrid = (long) args[3],
            AnchorRow = (double) args[4],
            AnchorCol = (double) args[5],
            Focusable = (bool) args[6]
          });
          break;

      case "win_external_pos":
          WinExternalPosEvent?.Invoke(this, new WinExternalPosEventArgs
          {
            Grid = (long) args[0],
            Win = (NvimWindow) args[1]
          });
          break;

      case "win_hide":
          WinHideEvent?.Invoke(this, new WinHideEventArgs
          {
            Grid = (long) args[0]
          });
          break;

      case "win_close":
          WinCloseEvent?.Invoke(this, new WinCloseEventArgs
          {
            Grid = (long) args[0]
          });
          break;

      case "msg_set_pos":
          MsgSetPosEvent?.Invoke(this, new MsgSetPosEventArgs
          {
            Grid = (long) args[0],
            Row = (long) args[1],
            Scrolled = (bool) args[2],
            SepChar = (string) args[3]
          });
          break;

      case "win_viewport":
          WinViewportEvent?.Invoke(this, new WinViewportEventArgs
          {
            Grid = (long) args[0],
            Win = (NvimWindow) args[1],
            Topline = (long) args[2],
            Botline = (long) args[3],
            Curline = (long) args[4],
            Curcol = (long) args[5]
          });
          break;

      case "popupmenu_show":
          PopupmenuShowEvent?.Invoke(this, new PopupmenuShowEventArgs
          {
            Items = (dynamic[]) args[0],
            Selected = (long) args[1],
            Row = (long) args[2],
            Col = (long) args[3],
            Grid = (long) args[4]
          });
          break;

      case "popupmenu_hide":
          PopupmenuHideEvent?.Invoke(this, EventArgs.Empty);
          break;

      case "popupmenu_select":
          PopupmenuSelectEvent?.Invoke(this, new PopupmenuSelectEventArgs
          {
            Selected = (long) args[0]
          });
          break;

      case "tabline_update":
          TablineUpdateEvent?.Invoke(this, new TablineUpdateEventArgs
          {
            Current = (NvimTabpage) args[0],
            Tabs = (dynamic[]) args[1]
          });
          break;

      case "cmdline_show":
          CmdlineShowEvent?.Invoke(this, new CmdlineShowEventArgs
          {
            Content = (dynamic[]) args[0],
            Pos = (long) args[1],
            Firstc = (string) args[2],
            Prompt = (string) args[3],
            Indent = (long) args[4],
            Level = (long) args[5]
          });
          break;

      case "cmdline_pos":
          CmdlinePosEvent?.Invoke(this, new CmdlinePosEventArgs
          {
            Pos = (long) args[0],
            Level = (long) args[1]
          });
          break;

      case "cmdline_special_char":
          CmdlineSpecialCharEvent?.Invoke(this, new CmdlineSpecialCharEventArgs
          {
            C = (string) args[0],
            Shift = (bool) args[1],
            Level = (long) args[2]
          });
          break;

      case "cmdline_hide":
          CmdlineHideEvent?.Invoke(this, new CmdlineHideEventArgs
          {
            Level = (long) args[0]
          });
          break;

      case "cmdline_block_show":
          CmdlineBlockShowEvent?.Invoke(this, new CmdlineBlockShowEventArgs
          {
            Lines = (dynamic[]) args[0]
          });
          break;

      case "cmdline_block_append":
          CmdlineBlockAppendEvent?.Invoke(this, new CmdlineBlockAppendEventArgs
          {
            Lines = (dynamic[]) args[0]
          });
          break;

      case "cmdline_block_hide":
          CmdlineBlockHideEvent?.Invoke(this, EventArgs.Empty);
          break;

      case "wildmenu_show":
          WildmenuShowEvent?.Invoke(this, new WildmenuShowEventArgs
          {
            Items = (dynamic[]) args[0]
          });
          break;

      case "wildmenu_select":
          WildmenuSelectEvent?.Invoke(this, new WildmenuSelectEventArgs
          {
            Selected = (long) args[0]
          });
          break;

      case "wildmenu_hide":
          WildmenuHideEvent?.Invoke(this, EventArgs.Empty);
          break;

      case "msg_show":
          MsgShowEvent?.Invoke(this, new MsgShowEventArgs
          {
            Kind = (string) args[0],
            Content = (dynamic[]) args[1],
            ReplaceLast = (bool) args[2]
          });
          break;

      case "msg_clear":
          MsgClearEvent?.Invoke(this, EventArgs.Empty);
          break;

      case "msg_showcmd":
          MsgShowcmdEvent?.Invoke(this, new MsgShowcmdEventArgs
          {
            Content = (dynamic[]) args[0]
          });
          break;

      case "msg_showmode":
          MsgShowmodeEvent?.Invoke(this, new MsgShowmodeEventArgs
          {
            Content = (dynamic[]) args[0]
          });
          break;

      case "msg_ruler":
          MsgRulerEvent?.Invoke(this, new MsgRulerEventArgs
          {
            Content = (dynamic[]) args[0]
          });
          break;

      case "msg_history_show":
          MsgHistoryShowEvent?.Invoke(this, new MsgHistoryShowEventArgs
          {
            Entries = (dynamic[]) args[0]
          });
          break;

      }
    }

    /*
    private object GetExtensionType(MessagePackExtendedTypeObject msgPackExtObj)
    {
      switch (msgPackExtObj.TypeCode)
      {

        case 0:
          return new NvimBuffer(this, msgPackExtObj);
        case 1:
          return new NvimWindow(this, msgPackExtObj);
        case 2:
          return new NvimTabpage(this, msgPackExtObj);
        default:
          throw new SerializationException(
            $"Unknown extension type id {msgPackExtObj.TypeCode}");
      }
    }
    */
  }
}