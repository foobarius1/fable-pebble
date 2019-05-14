module App

open Fable.Core
open Pebble.UI


let run() =
    let menu =
        let primaryColor = Color.Hex "#ffee00"
        let secondaryColor = Color.Named "black"
        [ MenuOption.TextColor primaryColor
          MenuOption.BackgroundColor secondaryColor
          MenuOption.HighlightTextColor secondaryColor
          MenuOption.HighlightBackgroundColor primaryColor
          MenuOption.Sections
            [| Menu.section
                [ SectionOption.Title "Hello"
                  SectionOption.Items
                    [| Menu.sectionItem
                        [ SectionItemOption.Title "Hello"
                          SectionItemOption.Subtitle "from F# land!" ] |] ] |] ]
        |> Menu.menu

    menu.status (U2.Case1 false)
    menu.show()
    menu.setSelection(0, 0)
