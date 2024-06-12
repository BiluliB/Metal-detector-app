# Magnetify

Magnetify is a .NET application built with the Microsoft Maui framework. It's a cross-platform application that can run on multiple platforms including Android and iOS.

## Setup and Installation

1. Ensure you have the .NET 8.0 SDK installed on your machine. You can download it from the [official .NET website](https://dotnet.microsoft.com/download).

2. Clone the repository to your local machine using Git:

```sh
git clone https://github.com/yourusername/ict-335-praxisarbeit.git
```

3. Navigate to the `Magnetify` directory:

```sh
cd ict-335-praxisarbeit/Magnetify
```

4. Restore the NuGet packages:

```sh
dotnet restore
```

## Running the Application

To run the application, use the `dotnet run` command:

```sh
dotnet run
```

## Running Tests

Tests are located in the [`Magnetify.Tests`](command:_github.copilot.openRelativePath?%5B%7B%22scheme%22%3A%22file%22%2C%22authority%22%3A%22%22%2C%22path%22%3A%22%2FC%3A%2FGithub%2Fict-335-praxisarbeit%2FMagnetify.Tests%22%2C%22query%22%3A%22%22%2C%22fragment%22%3A%22%22%7D%5D "c:\Github\ict-335-praxisarbeit\Magnetify.Tests") directory. To run the tests, navigate to the [`Magnetify.Tests`](command:_github.copilot.openRelativePath?%5B%7B%22scheme%22%3A%22file%22%2C%22authority%22%3A%22%22%2C%22path%22%3A%22%2FC%3A%2FGithub%2Fict-335-praxisarbeit%2FMagnetify.Tests%22%2C%22query%22%3A%22%22%2C%22fragment%22%3A%22%22%7D%5D "c:\Github\ict-335-praxisarbeit\Magnetify.Tests") directory and use the `dotnet test` command:

```sh
cd ../Magnetify.Tests
dotnet test
```

## Dependencies

The project depends on several NuGet packages, including:

- Microsoft.Maui.Controls: Provides the UI controls for the Maui application.
- Microsoft.NET.Test.Sdk: The MSBuild targets and properties for building .NET Test projects.
- Moq: A popular and friendly mocking framework for .NET.
- xunit: A free, open-source, community-focused unit testing tool for the .NET Framework.

## Overview

Magnetify is a project created as part of the ict-335-praxisarbeit. It's built using the Microsoft Maui framework, which allows for the creation of cross-platform applications using .NET. The application's main functionality and logic are contained within the `Magnetify` project, while unit tests can be found in the `Magnetify.Tests` project.

For more detailed information about the project structure and specific functionalities, please refer to the source code and inline comments.

## Contributing

Contributions are welcome! Please feel free to submit a pull request or open an issue on the GitHub repository.

## License

This project is licensed under the terms of the [Apache License 2.0](LICENSE).
