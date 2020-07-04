import React from 'react';
import {View} from "react-native";
import {StoreState} from "../data/store";
import {Dispatch} from "redux";
import {List} from "react-native-paper"
import {connect, ConnectedProps} from "react-redux";
import {HardwareChange} from "../core/server";
import {Computer} from "../data/computer-manager/reducer";


const mapStateToProps = (state: StoreState) => ({
    computer: state.computer.current
})

const mapDispatchToProps = (dispatch: Dispatch) => ({})

const connector = connect(mapStateToProps, mapDispatchToProps);
type ReduxTypes = ConnectedProps<typeof connector>;

interface Props extends ReduxTypes {

}

interface ActionsProps {
    computer: Computer,
    text: string,
    icon: string
    onPress: (computer: Computer) => void
}
function Action(props: ActionsProps) {
    const {onPress, computer, icon,text} = props;
    return  <List.Item left={props => <List.Icon {...props} icon={icon}/>} title={text} onPress={() => onPress(computer as Computer)}/>
}

export function Body(props: Props) {

    const [expandedInfo, setExpantedInfo] = React.useState(true);
    const [expandedActions, setExpantedActions] = React.useState(true);


    const infos = props.computer ? <>
        <List.Accordion
            title="Informations"
            expanded={expandedInfo}
            titleStyle={{fontWeight: "700"}}
            onPress={() => setExpantedInfo(!expandedInfo)}
        >
            <List.Item title={"Name"} description={props.computer.name}/>
            <List.Item title={"Host"} description={props.computer.host}/>
        </List.Accordion>
    </> : null
    const actions = props.computer ? <>
        <List.Accordion
            title="Actions"
            expanded={expandedActions}
            titleStyle={{fontWeight: "700"}}
            onPress={() => setExpantedActions(!expandedActions)}
        >
            <Action computer={props.computer as Computer} text={"Restart"} icon={"restart"} onPress={HardwareChange.reboot}/>
            <Action computer={props.computer as Computer} text={"Shutdown"} icon={"power"} onPress={HardwareChange.shutdown}/>
            <Action computer={props.computer as Computer} text={"Sleep"} icon={"sleep"} onPress={HardwareChange.sleep}/>

        </List.Accordion>
    </> : null

    return (
        <View>
            {infos}
            {actions}
        </View>
    );
}

export default connector(Body);
