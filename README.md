barcodelib
==========

### Overview ###

This library was designed to give an easy class for developers to use when they need to generate barcode images from a string of data.

### Supported Encoding Types ###
Code 128	
Code11	
Code 39 (Extended / Full ASCII)
Code 93	
EAN-8	
EAN-13
UPC-A	
UPC-E	
JAN-13
MSI	ISBN	
Standard 2 of 5
Interleaved 2 of 5	
PostNet	UPC Supplemental 2
UPC Supplemental 5	
Codabar	
ITF-14
Telepen	
Pharmacode	
FIM (Facing Identification Mark)

### Usage ###

The library contains a class called BarcodeLib. There are three constructors:
```
Barcode();
Barcode(string);
Barcode (string, BarcodeLib.TYPE);
```

If you decide to create an instance with parameters, the parameters are as follows: the string is the data to be encoded into the barcode, and BarcodeLib.TYPE is the symbology to encode the data with. If you do not choose to specify the data and type at the time the instance is created, you may specify them through the appropriate property later on (but before you encode).

```
BarCodeLib.Barcode b = new BarCodeLib.Barcode(BarCodeLib.TYPE.UPCA, 
          "038000356216", Color.Black, Color.White, 300, 150);
```
### Example ###
```
BarCodeLib.Barcode b = new BarCodeLib.Barcode(BarCodeLib.TYPE.UPCA, 
          "038000356216", Color.Black, Color.White, 300, 150);
Image img = b.Encode();
```

### Copyright and license ###

Copyright 2007-2014 Brad Barnhill. Code released under the [Apache License, Version 2.0](https://github.com/bbarnhill/barcodelib/blob/master/LICENSE).
