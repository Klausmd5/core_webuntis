# Plugin-Template

## Repository configuration

A git submodule is used to reference projects from the main core repository.

To initialize the core submodule, use the following command after cloning this repository:

```
git submodule update --init --recursive
```

## Pre-commit hooks

This repository uses [pre-commit](https://pre-commit.com/) to run a set of checks before committing changes.

> For further information on how to run pre-commit and the checks performed refer to the the [core readme](https://github.com/htl-grieskirchen-core/core#pre-commit-hooks).

## Development

Develop your plugin in the `CorePlugin.Plugin` project. You can then start the `CorePlugin.BackendDevServer` to run your
plugin.

> ⚠ You should not change the `CorePlugin.BackendDevServer` as it already calls the configure methods of your plugin and is only used in development thus it will not be included in the deployment of your plugin.

### Plugin-Interface

Plugins need to implement the `ICorePlugin` Interface of the core project in order to be loaded by the backend. WIth this interface you can implement the two methods `Configure` and `ConfigureServices`.

`Configure` - Using this method you can register your controllers. You are given the `app` object to configure the app's request handling pipeline and their middleware components.

`ConfigureServices` - Using this method you can register your classes for dependency injection. You are given the `builder` object of the backend to add new services.

### Frontend

`frontend` contains an empty angular project with tailwindcss already configured.

### Calling secure-backend APIs
Use the method `sendRequest` of the `api-connector` lib to call any method on the secure-backend which is permitted for your plugin in the `mainframe-config`.

There should be utility services for all APIs in the plugin-template (WIP at the time of writing).
