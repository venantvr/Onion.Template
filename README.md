# Onion.Template

Template d'architecture Onion en .NET : domaine avec bounded contexts, persistance MongoDB et SQL Server, bus d'evenements et tests.

## Structure

| Fichier / Dossier | Role |
|-|-|
| `Onion.Console/` | Application console (point d'entree) |
| `Onion.Console/Persistence/` | Implementations MongoDB et SQL Server |
| `Onion.Console/ServiceBus/` | Bus d'evenements |
| `Onion.Console/BootStrapper/` | Configuration et injection de dependances |
| `Onion.Domain/` | Domaine (Base, BoundedContext, Dtos, Notifications, Repositories) |
| `Onion.Domain.Tests/` | Tests unitaires du domaine |

## Stack

- C# / .NET
- Architecture Onion
- MongoDB
- SQL Server
- Bus d'evenements
