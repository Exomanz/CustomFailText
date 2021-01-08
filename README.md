# CustomFailText
Changes the default `LEVEL FAILED` text effect to something more customizable. It's your text; make it what you want.

All credits for the source code will go to [Arti](https://gitlab.com/artemiswkearney "The Original Modder").

## Features
* Picks a random entry from the selected config file at `Beat Saber/UserData/CustomFailText/`.
* Offers TextMeshPro styling, including colors, size, and much more. See the [TextMeshPro documentation](http://digitalnativestudios.com/textmeshpro/docs/rich-text/ "TextMeshPro Docs") for more information.
* Works in all game modes.
* Multi-config support, and basic settings from in-game UI panel.

## Roadpath
* Optimization to make the plugin lighter.
* Migrate settings to menu button.
* Allow customization of the effect backlighting (disable, and set colors).
* Hot-reloading of config files.

## Dependencies
* BSIPA v4.1.3+
* BS Utils v1.6.5+
* BeatSaberMarkupLanguage v1.4.2+

## Config File Format
Config files must be `.txt` files, and be formatted as follows.
```
# Comments are always notated with #'s. If you don't have one, it will be marked as an entry.
# Text entries will be chained as one until an empty line is detected.
THIS IS
ALL ONE
ENTRY

This is also one entry.

# Don't forget that TMP Styling can be used here, too! You can leave your tags unclosed 
# as long as it's either the end of the entry, or you want all of the entry to be that one tag.
<size=+30>Hello world!

Hello world! (but smaller)
# The entry above isn't affected by the size tag that was left unclosed since it's a different entry.
```

## Installation
Grab the latest version from the [Releases](https://github.com/Exomanz/CustomFailText/releases/latest "releases") page and install it in your Plugins folder at your Beat Saber directory.

## Contributing to CustomFailText
Download the source code, and open the project in Visual Studio 2019. You may need to resolve your references, but after that you should be able to build the project. If you have any suggestions, feel free to submit a pull request!
