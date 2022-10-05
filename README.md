# MaskClassParser
Class-data parser. It has a built-in random content generator.

Parsing occurs by mask: 
1) [Name] -> if name not null - print
2) {Name, Age} -> if Name and Age not null - print
3) [some]&lt;,>[some2] -> if some and some2 not null - print symbol beetwen &lt;>
