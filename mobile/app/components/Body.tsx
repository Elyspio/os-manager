import React, {Component} from 'react';
import {View} from "react-native";
import {StoreState} from "../data/store";
import {Dispatch} from "redux";
import {Text} from "react-native-paper"
import {connect, ConnectedProps} from "react-redux";


const mapStateToProps = (state: StoreState) => ({
    computer: state.computer.current
})

const mapDispatchToProps = (dispatch: Dispatch) => ({})

const connector = connect(mapStateToProps, mapDispatchToProps);
type ReduxTypes = ConnectedProps<typeof connector>;

interface Props extends ReduxTypes {

}

export class Body extends Component<Props> {
    render() {
        return (
            <View>
                <Text>Computer: {this.props.computer ? this.props.computer.name : null}</Text>
            </View>
        );
    }
}

export default connector(Body);
