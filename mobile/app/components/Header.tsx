import React from 'react';
import {Appbar, Button, Dialog, Portal, Text} from 'react-native-paper';
import {StoreState} from "../data/store";
import {connect, ConnectedProps} from "react-redux";
import {Dispatch} from "redux";
import {setSelectedComputer} from "../data/computer-manager/actions";
import {Computer} from "../data/computer-manager/reducer";

const mapStateToProps = (state: StoreState) => ({
    computer: state.computer.current
})

const mapDispatchToProps = (dispatch: Dispatch) => ({
    setCurrent: (computer: Computer) => {dispatch(setSelectedComputer(computer))}
})

const connector = connect(mapStateToProps, mapDispatchToProps);
type ReduxTypes = ConnectedProps<typeof connector>;

interface Props extends  ReduxTypes{
    title: string,
    subtitle?: string
}

export function Header(props: Props) {

    const [visible, setVisible] = React.useState(false);


    const showDialog = () => setVisible(true);
    const hideDialog = () => setVisible(false);

    return (
        <Appbar.Header>
            <Appbar.Content title={props.title} subtitle={props.subtitle}/>
            <Appbar.Action icon="server-network" animated onPress={showDialog}/>
            <SelectComputer visible={visible} hide={hideDialog} checked={{set: props.setCurrent, get: props.computer}}/>
        </Appbar.Header>
    );
}


interface SelectComputerProps {
    visible: boolean,
    hide: () => void
    checked: { set: (val: Computer) => void, get?: Computer }
}


const SelectComputer = (props: SelectComputerProps) => {
    const {visible, hide, checked} = props

    function getComputers() {
        const mock = [
            {host: "elyspio.fr/aero/command", name: "aero-oled"},
            {host: "elyspio.fr/elyspi/command", name: "elyspi"}
        ]

        return mock.map(computer => {
                const handlePress = () => {
                    checked.set(computer);
                    hide();
                }
                return (
                    <Button onPress={handlePress} mode={computer.host === checked.get?.host ? "contained" : "outlined"}>
                        <Text numberOfLines={1}>
                            {computer.name} - {computer.host}
                        </Text>
                    </Button>
                )

            }
        )
    }

    const computers = getComputers();
    return (
        <Portal>
            <Dialog visible={visible} onDismiss={hide}>
                <Dialog.Title>Select a computer</Dialog.Title>
                <Dialog.Content>
                    {computers}
                </Dialog.Content>

            </Dialog>
        </Portal>
    );
};


export default connector(Header);
