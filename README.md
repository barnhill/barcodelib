# barcodelib ![Barcode CI](https://github.com/barnhill/barcodelib/workflows/Barcode%20CI/badge.svg) [![NuGet](https://img.shields.io/nuget/v/BarcodeLib.svg)](https://www.nuget.org/packages/BarcodeLib)

## Overview

This library was designed to give an easy class for developers to use when they need to generate barcode images from a string of data.

|   Supported   |  Symbology    | List  |
| :------------- | :------------- | :-----|
| Code 128      | Code 93       | Code 39 (Extended / Full ASCII) |
| Code11        | EAN-8         | FIM (Facing Identification Mark) |
| UPC-A         | UPC-E         | Pharmacode   |
| MSI           | PostNet       | Standard 2 of 5 |
| ISBN          | Codabar       | Interleaved 2 of 5 |
| ITF-14        | Telepen       | UPC Supplemental 2 |
| JAN-13        | EAN-13        | UPC Supplemental 5 |
| IATA2of5      |               |                    |

## Usage

The library contains a class called `Barcode` with three constructors:

```csharp
Barcode();
Barcode(string);
Barcode(string, Type);
```

If you decide to create an instance with parameters, the parameters are as follows: the string is the data to be encoded into the barcode, and `Type` is the symbology to encode the data with. If you do not choose to specify the data and type at the time the instance is created, you may specify them through the appropriate property later on (but before you encode).

## Example

```csharp
var b = new Barcode();
b.IncludeLabel = true;
var img = b.Encode(Type.UpcA, "038000356216", SKColors.Black, SKColors.White, 290, 120);
```

![Alt text](BarcodeStandard/examples/upca.jpg?raw=true "UPC-A")

## Support

If you find this or any of my software useful and decide its worth supporting.  You can do so here:  [![Donate](https://img.shields.io/badge/Donate-PayPal-green.svg)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=QKT9PSYTDNSXS)

## Copyright and license

Copyright 2007-2025 Brad Barnhill. Code released under the [Apache License, Version 2.0](https://github.com/bbarnhill/barcodelib/blob/master/LICENSE).
