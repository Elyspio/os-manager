import React from 'react';
import {SafeAreaView, StyleSheet, View} from 'react-native';
import {DefaultTheme, Provider as PaperProvider} from 'react-native-paper';
import {Provider as ReduxProvider} from "react-redux"
import Header from "./app/components/Header";
import {store} from "./app/data/store";
import Body from "./app/components/Body"
export function App() {


    return (
        <SafeAreaView style={styles.container}>
            <Header title={"Android Windows Link"}/>
            <Body/>

        </SafeAreaView>
    );
}

const theme = {
    ...DefaultTheme,
    dark: true,
    colors: {
        ...DefaultTheme.colors,
        primary: '#3498db',
        accent: '#f1c40f',
    },
};

export default function Main() {
    return (
        <PaperProvider theme={theme}>
            <ReduxProvider store={store}>
                <App/>
            </ReduxProvider>
        </PaperProvider>
    );
}

const styles = StyleSheet.create({
    container: {},


});
