# CustomFailText
Changes the default `LEVEL FAILED` text effect to something more customizable.

All credits for the original source code will go to [Arti](https://gitlab.com/artemiswkearney "The Original Modder").

## Features
* Picks a random entry from the selected config file at `Beat Saber/UserData/CustomFailText/`. You may also create your own here (see formatting below).
* Offers TextMeshPro styling, including colors, size, and much more. See the [TextMeshPro documentation](http://digitalnativestudios.com/textmeshpro/docs/rich-text/ "TextMeshPro Docs") for more information.
* Supports multiple config files and custom fail effect colors.

## Dependencies
* BSIPA v4.2.0+
* SiraUtil v2.5.6+
* BeatSaberMarkupLanguage v1.5.4+

## Config File Format
Config files must be `.txt` files, and be formatted as follows.
```
# Comments are always notated with #'s. If you don't have one, it will be marked as an entry.
# Text entries will be chained as one until an empty line is detected.
THIS IS
ALL ONE
ENTRY

This is also one entry.

# Don't forget that TMP Styling can be used here, too! You can leave your tags unclosed (despite what it
# says in the config) as long as it's either the end of the entry, or you want all of the entry to be that one tag.
<size=+30>Hello world!

Hello world! (but smaller)
# The entry above isn't affected by the size tag that was left unclosed since it's a different entry.
```

## Installation
Grab the latest version from the [releases](https://github.com/Exomanz/CustomFailText/releases/latest "releases") page and install it in your Plugins folder at your Beat Saber directory. The mod will do all of the directory and file creation for you, unless you're making a custom text file, in which case you have to do it yourself.
