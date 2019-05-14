module Pebble.Libraries

open Fable.Core
open Fable.Core.JsInterop


module Ajax =

    [<StringEnum>]
    [<RequireQualifiedAccess>]
    type Method =
        | Get
        | Post
        | Put
        | Delete
        | Options

    [<StringEnum>]
    [<RequireQualifiedAccess>]
    type ContentType =
        | Json
        | Text

    [<RequireQualifiedAccess>]
    type AjaxOption<'a> =
        | Url of string
        | Method of Method
        | [<CompiledName("type")>] ContentType of ContentType
        | Data of 'a
        //| Headers of obj
        //| Async of bool
        | Cache of bool

    type StatusCode = StatusCode of int

    type Response = obj

    type Callback<'a> = 'a -> StatusCode -> Response -> unit

    let ajax (options: AjaxOption<'a> list) (onSuccess: Callback<'a>) (onFailure: Callback<'a>) =
        let optionsObj = keyValueList CaseRules.LowerFirst options
        importDefault "ajax" (optionsObj,
            (fun (data, statusCode, response) -> onSuccess data (StatusCode statusCode) response),
            (fun (data, statusCode, response) -> onFailure data (StatusCode statusCode) response))

