# ShellTest

Repo created to demonstrate an issue with ToolbarItem binding on the Xamarin Forms Shell app.

Steps taken.
1. Create new project using the Shell template.
2. Modify ItemsViewModel.cs
  a) Add ToggleCommand and ToolbarCommand, including the initialisation and execute/can execute delegates.
  b) Add _state field.
3. Modify ItemPage.xaml
  a) Remove clicked handler from ToolbarItem, replace with Command="{Binding ToolbarCommand}"
  b) Add Grid above ListView
  c) Add button to first column, bound to ToggleCommand
  d) Add button to second column, bound to ToolbarCommand.
4. Update Xamarin Forms version to 4.3.0.991211



Results Expected;
ToolbarItem enabled state and function to behave as per the added Check button

Results Found;
ToolbarItem appears disabled and does not respond to the Toggle being fired.

Additional;
If you use Hot Reload to position the buttons under the grid, whilst _state is still true, the view recompiles and the ToolbarItem is now enabled and functions, though it does not respond to further toggle updates.
Further, if you use hot reload to place the buttons back into grid whilst _state is false, the view recompiles and the ToolbarItem is now disabled, does not function and does not respond to toggle updates.
