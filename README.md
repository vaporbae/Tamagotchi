# Tamagotchi


## Gra konsolowa: wersja .NET Framework 4.7.2


## Liczenie linii kodu:
https://stackoverflow.com/questions/45267005/visual-studio-2017-measuring-lines-of-code

A little hacky way that works quite well is to use a RegEx with Find in Files

Ctrl-Shift-F or Edit -> Find and Replace -> Find in Files
Use ^(?([^\r\n])\s)*[^\s+?/]+[^\n]*$ in the 'Find what:' field
Check 'Use Regular Expressions'
Set 'Look in:' and 'Look at these file types:' to your desired search scope
Hit enter and scroll to the bottom after the find is complete and you'll see Matching lines: 25843. That's the line count