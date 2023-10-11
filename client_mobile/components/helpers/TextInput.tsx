import { TextInput, TextInputProps } from "react-native-paper";
import styles from "../../constants/styles";

export default function Input(props: TextInputProps) {
  const style = styles.textInput
  return (
    <TextInput
      label={props?.label}
      value={props?.value}
      onChangeText={props?.onChangeText}
      mode="outlined"
      style={style}
      secureTextEntry={props?.secureTextEntry}
      keyboardType={props?.keyboardType}
      autoCapitalize={props?.autoCapitalize}
      placeholder={(props?.placeholder) ? props?.placeholder : 'Default'}
    />
  );
}
