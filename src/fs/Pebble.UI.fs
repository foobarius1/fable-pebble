module Pebble.UI

open Fable.Core
open Fable.Core.JsInterop


[<Erase>]
[<RequireQualifiedAccess>]
type Color =
    | Named of string
    | Hex of string

[<Erase>]
type Image =
    Image of string


type ActionDef =
    { up: Image option
      down: Image option
      select: Image option
      backgroundColor: Color }


[<StringEnum>]
type SeperatorStyle =
    | Dotted
    | [<CompiledName("none")>] NoSeperator

// TODO: explore a prop list based api instead
// of records similar to the react libs
//[<RequireQualifiedAccess>]
//type StatusDefOptions =
//    | Seperator of SeperatorStyle
//    | Color of Color
//    | BackgroundColor of Color

type StatusDef =
    { seperator: SeperatorStyle
      color: Color
      backgroundColor: Color }

let defaultStatusDef =
    { seperator = Dotted
      color = Color.Named "black"
      backgroundColor = Color.Named "white" }

[<StringEnum>]
type ClickType =
    | Click
    | LongClick

[<StringEnum>]
type ButtonType =
    | Up
    | Down
    | Select
    | Back

type IWindowBase =
    abstract show: unit -> unit
    abstract hide: unit -> unit

    abstract on: ClickType -> ButtonType -> (unit -> unit)
    [<Emit("$0.on('show', $1)")>] abstract onShow: (unit -> unit) -> unit
    [<Emit("$0.on('hide', $1)")>] abstract onHide: (unit -> unit) -> unit

    abstract action: U2<bool, ActionDef> -> unit
    abstract status: U2<bool, StatusDef> -> unit


[<StringEnum>]
type CardStyle =
    | Small
    | Large
    | Mono

type ICard =
    inherit IWindowBase

    abstract title: unit -> string
    abstract titleColor: unit -> Color
    abstract subtitle: unit -> string
    abstract subtitleColor: unit -> Color
    abstract body: unit -> string
    abstract bodyColor: unit -> Color
    abstract icon: unit -> Image option
    abstract subicon: unit -> Image option
    abstract banner: unit -> Image option
    abstract scrollable: unit -> bool
    abstract style: unit -> CardStyle

    [<Emit("$0.title($1)")>]         abstract setTitle: string -> unit
    [<Emit("$0.titleColor($1)")>]    abstract setTitleColor: Color -> unit
    [<Emit("$0.subtitle($1)")>]      abstract setSubtitle: string -> unit
    [<Emit("$0.subtitleColor($1)")>] abstract setSubtitleColor: Color -> unit
    [<Emit("$0.body($1)")>]          abstract setBody: string -> unit
    [<Emit("$0.bodyColor($1)")>]     abstract setBodyColor: Color -> unit
    [<Emit("$0.icon($1)")>]          abstract setIcon: Image option -> unit
    [<Emit("$0.subicon($1)")>]       abstract setSubicon: Image option -> unit
    [<Emit("$0.banner($1)")>]        abstract setBanner: Image option -> unit
    [<Emit("$0.scrollable($1)")>]    abstract setScrollable: bool -> unit
    [<Emit("$0.style($1)")>]         abstract setStyle: CardStyle -> unit


// TODO: replace with options api like for menu
type CardOptions =
    { title: string
      titleColor: Color
      subtitle: string
      subtitleColor: Color
      body: string
      bodyColor: Color
      icon: Image option
      subicon: Image option
      banner: Image option
      scrollable: bool
      style: CardStyle

      // inherited from Window
      action: U2<bool, ActionDef>
      status: U2<bool, StatusDef> }

let defaultCardOptions =
    { title = ""
      titleColor = Color.Named "black"
      subtitle = ""
      subtitleColor = Color.Named "black"
      body = ""
      bodyColor = Color.Named "black"
      icon = None
      subicon = None
      banner = None
      scrollable = false
      style = Large
      action = U2.Case1 false
      status = U2.Case1 true }

type CardStatic =
    [<Emit("new $0($1)")>]
    abstract create: CardOptions -> ICard


[<Erase>]
type SectionItem = private SectionItem of obj

[<RequireQualifiedAccess>]
type SectionItemOption =
    | Title of string
    | Subtitle of string
    | Icon of Image

[<Erase>]
type Section = private Section of obj

[<RequireQualifiedAccess>]
type SectionOption =
    | Title of string
    | TextColor of Color
    | BackgroundColor of Color
    | Items of SectionItem array

type IMenu =
    inherit IWindowBase

    abstract textColor: unit -> Color
    abstract backgroundColor: unit -> Color
    abstract highlightTextColor: unit -> Color
    abstract highlightBackgroundColor: unit -> Color
    abstract sections: unit -> Section list

    [<Emit("$0.textColor($1)")>]                 abstract setTextColor: Color -> unit
    [<Emit("$0.backgroundColor($1)")>]           abstract setBackgroundColor: Color -> unit
    [<Emit("$0.hightlightTextColor($1)")>]       abstract setHightlightTextColor: Color -> unit
    [<Emit("$0.hightlightBackgroundColor($1)")>] abstract setHightlightBackgroundColor: Color -> unit
    [<Emit("$0.sections($1)")>]                  abstract setSections: Section list -> unit
    [<Emit("$0.section($1, $2)")>]               abstract setSection: int * Section -> unit
    [<Emit("$0.items($1, $2)")>]                 abstract setItems: int * SectionItem list -> unit
    [<Emit("$0.item($1, $2, $3)")>]              abstract setItem: int * int -> SectionItem -> unit
    [<Emit("$0.selection($1, $2)")>]             abstract setSelection: int * int -> unit
    // TODO:
    // selection() with callback (event or getter??)
    // onSelect
    // onLongSelect

[<RequireQualifiedAccess>]
type MenuOption =
    | TextColor of Color
    | BackgroundColor of Color
    | HighlightTextColor of Color
    | HighlightBackgroundColor of Color
    | Sections of Section array

type MenuStatic =
    [<Emit("new $0($1)")>]
    abstract create: obj -> IMenu


type IUI =
    abstract Card: CardStatic
    abstract Menu: MenuStatic

let private UI: IUI = importDefault "pebblejs/ui"


module Menu =
    let menu (options: MenuOption list) =
        UI.Menu.create (keyValueList CaseRules.LowerFirst options)

    let section (options: SectionOption list) =
        keyValueList CaseRules.LowerFirst options |> Section

    let sectionItem (options: SectionItemOption list) =
        keyValueList CaseRules.LowerFirst options |> SectionItem
