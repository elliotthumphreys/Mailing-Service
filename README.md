<h1 align="center">Mailing Service</h1>

<div align="center">
  The Simple Mailing Service WebAPI in conjunction with the Content Builder class library allows for the ease of sending consistently formatted emails from a backend service.
</div>

<br />

## Usage - WebAPI & Class Library ##

```C#
var content = new HTMLBodyBuilder()
      .StartHeader()
          .AddH1("Example Heading")
          .AddH2("Example Sub Heading")
          .AddP("Some smaller leading paragraph")
      .EndHeader()
      .AddP("Example reference number: <strong>Example-Ref</strong>")
      .AddH2("Another example heading.")
      .AddP("Some smaller leading paragraph.")
      .AddH3("Here is a random heading")
      .AddP("Some smaller leading paragraph.")
      .AddButton("Go to Link", "http://www.google.com")
      .Build();

var request = new SendMailRequest
{
    Body = content,
    Subject = "Example Email Subject",
    Client = ClientEnum.Default,
    Recipients = new List<Recipient>
    {
        new Recipient
        {
            Email = "example-email@example.co.uk",
            Name = "Example Name"
        }
    }
};

// POST request to [your mailing service host]/api/v1/mail
```

## WebAPI - SimpleMailingService ##

### Configuration ###

Add your configuration for testing and production environments.

```json5
"TokenAuthentication": {
    "Key": "Your secret token here",
},
"Clients": {
    "Values": {
      "Default": {
        "Name": "Example Name",
        "Email": "no-reply@example.uk",
        "Host": "smtp.example.uk",
        "Port": 587,
        "UseSSL": false,
        "Authentication": {
          "Username": "your password",
          "Password": "your email"
        }
      },
      "Default": {
        "Name": "Other Name",
        "Email": "no-reply@other.uk",
        "Host": "smtp.other.uk",
        "Port": 587,
        "UseSSL": false,
        "Authentication": {
          "Username": "your other password",
          "Password": "your other email"
        }
      }
    }
}
```

Add an Enum for each of the "Client" objects in the configuration.

```c#
public enum ClientEnum
    {
        Default,
        Personal
    }
```

### Usuage ###

The WebAPI has only one endpoint `[POST] /api/v1/mail` which accepts a `SendMailRequest` model.

```json5
{
   "Recipients": [{
        "Name": "Example Name",
        "Email": "example@example-host.com"
    }],
   "Subject": "example email subject",
   "Body": "<h1>Example body content</h1>",
   "Client": 1, // ClientEnum.Personal
   "Attachments":
        [{
            "Name": "example attachment name",
            "Base64Content": "SGVsbG8sIFdvcmxk",
            "MimeType": {
                "Type": "text",
                "SubType": "plain"
        }]
    }
}
```

## Class library - ContentBuilder ##

Consistent styling has been confirmed across the following mail clients:
- Windows 10 Mail
- Outlook 2016
- Outlook for Anroid
- Outlook Web Client
- Gmail for Anroid
- Gmail Web Client

### Setup ###

Add a reference to the class library from your project.

### Usuage ###

```c#
var content = new HTMLBodyBuilder()
      .StartHeader()
          .AddH1("Example Heading")
          .AddH2("Example Sub Heading")
          .AddP("Some smaller leading paragraph")
      .EndHeader()
      .AddP("Example reference number: <strong>Example-Ref</strong>")
      .AddH2("Another example heading.")
      .AddP("Some smaller leading paragraph.")
      .AddH3("Here is a random heading")
      .AddP("Some smaller leading paragraph.")
      .AddButton("Go to Link", "http://www.google.com")
      .Build();
```

The above code produces the email content shown below.

<p align="center">
  <img src="https://iili.io/JvsPrQ.png" width="250" title="Mobile - dark mode">
  <img src="https://iili.io/Jvs6Ex.png" width="100%" title="Desktop - light mode">
</p>
