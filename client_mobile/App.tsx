import { View } from "react-native";
import { PaperProvider } from "react-native-paper";
import { AppBar } from "./components/helpers/AppBar";
import { LoginComponent } from "./components/auth/login";

export default function App() {
  return (
    <PaperProvider>
      <AppBar title="Elastic Ecommerce" />
      <View>
        <LoginComponent></LoginComponent>
      </View>
    </PaperProvider>
  );
}
