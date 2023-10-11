import { View, StyleSheet, Text } from "react-native";
import { AppBar } from "../helpers/AppBar";
import { Button } from "../helpers/Button";
import styles from "../../constants/styles";
import Input from "../helpers/TextInput";

export function LoginComponent(props: any) {
  const handleLogin = () => {
    console.log("Login!");
  };

  const handleSignup = () => {};
  // update style
  const style = StyleSheet.create({
    button: {
      ...styles.buttonDefault,
      width: "45%",
    },
    login: {
      ...styles.loginComponent,
    },
    inline: {
      flexDirection: "row",
      justifyContent: "space-between",
      alignItems: "center",
      width: "90%",
    },
  });
  return (
    <>
      <View style={style.login}>
        <Input
          placeholder="Please enter your email address"
          keyboardType="email-address"
          secureTextEntry={false}
        ></Input>
        <Text style={styles.hint}>Eg. user@domain.com</Text>
        <Input
          placeholder="Please enter your password"
          secureTextEntry={true}
        ></Input>
        <View style={style.inline}>
          <Button
            title="Login"
            onPress={handleLogin}
            style={style.button}
            mode="contained"
          ></Button>
          <Button
            title="Signup"
            onPress={handleSignup}
            style={style.button}
          ></Button>
        </View>
      </View>
    </>
  );
}
