import { Button as MaterialButton } from "react-native-paper";
import { ButtonProps } from "../../types/props/ButtonProps.type";

export function Button(props: ButtonProps) {
  return (
    <MaterialButton
      icon={props?.icon}
      mode={props?.mode}
      onPress={props?.onPress}
      style={props?.style}
    >
      {props?.title}
    </MaterialButton>
  );
}