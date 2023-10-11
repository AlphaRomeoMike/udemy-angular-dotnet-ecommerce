import { ImageStyle, TextStyle, ViewStyle } from "react-native"

export type ButtonProps = {
  icon?: string,
  mode?: "text" | "outlined" | "contained",
  onPress: (input: any) => void,
  style?: ViewStyle | TextStyle | ImageStyle,
  title: string,
}