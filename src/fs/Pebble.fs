module Pebble

open Fable.Core
open Fable.Core.JsInterop

[<Erase>]
type Color =
    | NamedColor of string
    | HexString of string
    | HexNumber of int

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

type StatusDef =
    { seperator: SeperatorStyle
      color: Color
      backgroundColor: Color }

let defaultStatusDef =
    { seperator = Dotted
      color = NamedColor "black"
      backgroundColor = NamedColor "white" }

[<Erase>]
type StatusType =
    | Enabled of bool
    | StatusDef of StatusDef


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

    // TODO: needs an enabled case like statustype
    abstract action: ActionDef -> unit
    abstract status: StatusType -> unit


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
      clear: bool
      actionDef: ActionDef option
      status: StatusType option }

let defaultCardOptions =
    { title = ""
      titleColor = NamedColor "black"
      subtitle = ""
      subtitleColor = NamedColor "black"
      body = ""
      bodyColor = NamedColor "black"
      icon = None
      subicon = None
      banner = None
      scrollable = false
      style = Large
      clear = false
      actionDef = None
      status = None }

type CardStatic =
    [<Emit("new $0($1)")>]
    abstract create: CardOptions -> ICard


type IUI =
    abstract Card: CardStatic


let UI: IUI = importDefault "pebblejs/ui"
