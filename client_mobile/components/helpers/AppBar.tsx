import { Appbar as MaterialAppBar } from "react-native-paper";

export function AppBar (props: any) {
  return (
    <MaterialAppBar.Header>
      <MaterialAppBar.Content title={props?.title} />
    </MaterialAppBar.Header>
  );
}
