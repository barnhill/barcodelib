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
#### Example ####
```
BarCodeLib.Barcode b = new BarCodeLib.Barcode(BarCodeLib.TYPE.UPCA, 
          "038000356216", Color.Black, Color.White, 300, 150);
Image img = b.Encode();
```

### Code 128 Barcodes ###

A Code 128 Barcode may contain any combination of 3 different codesets, Code A, Code B and Code C.

Using the library it is possible to generate a Code 128 barcode in a number of ways:

 - Either entirely of a single codeset (using types CODE128A, CODE128B or CODE128C)
 - Letting the generator decide which codeset to use for each character (or character pair) (using type CODE128)
 - Specifiying the codesets required for each portion of the barcode (using type CODE128CUSTOM)

 To specify the codesets required for each portion of the barcode there is a Code128CustomBuilder class with the below methods.
```
Code128CustomBuilder.Start(Code128Type, string); //Static constructor, takes the starting codeset required and the string to encode
AddPart(Code128Type, string); //Adds a new section to the barcode with the codeset specified
GetBarcodeString(); //Gives you a correctly formatted string to pass into a Barcode object of type CODE128CUSTOM
```
#### Example ####
```
var barcodeString =
    Code128Builder.Start(Code128Builder.Code128Type.B, "%0S23413S")
        .AddPart(Code128Builder.Code128Type.C, "15976911000231151826")
        .GetBarcodeString();
var barcode = b.Encode(TYPE.CODE128, barcodeString);
```