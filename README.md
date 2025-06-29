![QList Logo](https://raw.githubusercontent.com/QuantumLeap-Studios/QList/master/Media/qlist.png)

QList is a lightweight C# library for serializing and parsing structured data using a custom, human-readable format. It provides three main components:

- **QListObject**: A flexible container for key-value pairs.
- **QListSerializer**: Converts `QListObject` instances to string representations.
- **QListParser**: Parses strings into `QListObject` instances.

## Features

- Supports nested objects, lists, strings, numbers, and booleans.
- Simple, readable syntax for data interchange.
- .NET 8 and C# 12 compatible.

---

## Installation

Add the source files (`QListObject.cs`, `QListParser.cs`, `QListSerializer.cs`) to your project.

---

## Usage

### Creating a QListObject


```
using QList;

var obj = new QListObject();
obj["name"] = "Alice";
obj["age"] = 30;
obj["isActive"] = true;
obj["scores"] = new List<object> { 95, 88, 76 };

var address = new QListObject();
address["city"] = "Wonderland";
address["zip"] = "12345";
obj["address"] = address;

```

### Serializing


```
var serializer = new QListSerializer();
string serialized = serializer.Serialize(obj);
// Output example: {name="Alice";age=30;isActive=true;scores=[95,88,76];address={city="Wonderland";zip="12345";};}

```

### Parsing


```
var parser = new QListParser();
QListObject parsed = parser.Parse(serialized);
string name = parsed["name"] as string; // "Alice"

```

---

## Format Example


```
{
  name="Alice";
  age=30;
  isActive=true;
  scores=[95,88,76];
  address={city="Wonderland";zip="12345";};
}

```

---

## API Reference

### QListObject

- `Dictionary<string, object> Properties`  
  Stores the object's key-value pairs.
- `object this[string key]`  
  Indexer for getting/setting properties.

### QListSerializer

- `string Serialize(QListObject obj)`  
  Serializes a `QListObject` to a string.

### QListParser

- `QListObject Parse(string input)`  
  Parses a string into a `QListObject`.

---