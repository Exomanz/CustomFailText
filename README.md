# CustomFailText
An update for Arti's original CustomFailText for Beat Saber 1.11!

This plugin changes the default "LEVEL FAILED" text in Beat Saber to something more customizable.

All credits for this plugin and source code will go to [Arti](https://gitlab.com/artemiswkearney "The Original Modder"), as I just updated it for the most recent version.

## Features
* Picks a random entry from the config file at `/UserData/CustomFailText.txt`.
* Offers TextMeshPro styling, including colors, size, and much more. See the [TextMeshPro Documentation](http://digitalnativestudios.com/textmeshpro/docs/rich-text/ "TextMeshPro Docs") for more information.
* Works in all game modes **(including online)**!

## Roadpath
* Add support for multiple preset files and further mod config through in-game UI.
* Clean up and optimize the code.
* Suggestions are welcome!

## Current Known Issues
* Minor overlap occurs with some text strings.
* If you fail twice in one level, the text string will change twice. *Duplicable by failing, then hitting a note while they're still dissolving.*

## Dependencies
* BSIPA v4.0.9
* BS_Utils v1.6.5
* BSML v1.4.2 **(eventually)**

## Installation

Grab the latest version from the [Releases](https://github.com/Exomanz/CustomFailText/releases/latest "Releases") page and install it in your Plugins folder at your Beat Saber directory.

## Contributing to CustomFailText
Download the source code, and open the project in Visual Studio 2019. You may need to resolve your references, but after that you should be able to build the project. If you have any suggestions, feel free to submit a pull request!

## Example Screenshots
* Standard Text

![alt-text](https://github.com/Exomanz/CustomFailText/blob/main/Screenshots/standard%20text.png "Standard Text")

* TextMeshPro Colors

![alt-text](https://github.com/Exomanz/CustomFailText/blob/main/Screenshots/textmeshpro%20colors.png "TextMeshPro Colors")

* TextMeshPro Size

![alt-text](https://github.com/Exomanz/CustomFailText/blob/main/Screenshots/textmeshpro%20size.png "TextMeshPro Size")
