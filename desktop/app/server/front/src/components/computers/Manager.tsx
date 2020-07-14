import React, {Component} from 'react';
import {RootState} from "../../store/reducer";
import {connect, ConnectedProps} from "react-redux";
import "./Manager.scss";
import ComputerJSX from "./Computer"
import {Computer} from "../../../../../../../mobile/app/data/computer-manager/reducer";
import Typography from '@material-ui/core/Typography/Typography';

const mapStateToProps = (state: RootState) => ({
    lights: state.computer.computers
});
const mapDispatchToProps = (dispatch: Function) => ({});

const connector = connect(mapStateToProps, mapDispatchToProps);
type ReduxTypes = ConnectedProps<typeof connector>;
type Props = ReduxTypes

class Manager extends Component<Props> {

    render() {

        const computers = [...this.props.lights].sort((a: Computer, b: Computer) => {
            if (a.name < b.name) {
                return -1
            } else if (a.name === b.name) {
                return a.name < b.name ? -1 : 1;
            } else {
                return 1;
            }
        });


        return (
            <div className={"Manager"}>
                <Typography variant={"h4"}>Computers</Typography>
                <div className="computers">
                    {computers.map(l => <ComputerJSX key={l.name} data={l}/>)}
                </div>
            </div>
        );
    }
}

export default connector(Manager);
