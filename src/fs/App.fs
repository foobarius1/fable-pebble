module App

open Pebble

let run() =
    let text =
        [ "Hello"
          "from"
          "F#" ]
        |> String.concat " "

    let card =
        UI.Card.create
            { defaultCardOptions with
                title = "Hello World"
                body = text
                scrollable = true }

    card.show()
