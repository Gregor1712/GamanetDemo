# GamanetDemo

A WPF desktop application that displays person records loaded from a CSV file, with filtering and sorting capabilities. Built with Material Design styling.

## Features

- **Person Cards** — displays name, country, phone, and email in color-coded cards (grouped by country)
- **Country Filter** — autocomplete search box to filter persons by country
- **Sorting** — sort by name, country, or both via a dropdown menu
- **Material Design** — modern UI using [MaterialDesignInXAML](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit)

## Project Structure

| Project | Description |
|---|---|
| **GamanetDemo** | WPF application entry point (MainWindow, App configuration) |
| **DemoPanel** | WPF class library containing UI controls, view models, data access, and models |
| **Gamanet.Common** | Shared base classes (`PropertyChangedBase` for MVVM support) |

```
GamanetDemo/
├── GamanetDemo/          # Main WPF app
│   ├── csv/              # PersonsDemo.csv (100 sample records)
│   ├── Model/            # _AppContext
│   └── MainWindow.xaml
├── DemoPanel/            # UI library
│   ├── DataSource/       # CsvDataSource (CSV loading via CsvHelper)
│   ├── Model/            # PersonEntity, PersonRepository, PersonService
│   ├── UI/
│   │   ├── Control/      # PersonCard, AutoCompleteBox
│   │   ├── Converters/   # CountryToColorConverter
│   │   └── Panel/        # MainPanel, MainPanelViewModel
│   └── Images/
└── Gamanet.Common/       # Shared utilities
```

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Windows (WPF requires Windows)

## Getting Started

```bash
# Clone the repository
git clone <repo-url>
cd GamanetDemo

# Build
dotnet build

# Run
dotnet run --project GamanetDemo
```

## Dependencies

- [MaterialDesignThemes](https://www.nuget.org/packages/MaterialDesignThemes/) — Material Design UI components
- [CsvHelper](https://www.nuget.org/packages/CsvHelper/) — CSV file parsing