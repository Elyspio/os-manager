import * as React from 'react';
import Manager from "./computers/Manager";
import {Paper} from "@material-ui/core";
import "./Application.scss"
import {connect, ConnectedProps} from "react-redux";
import {Dispatch} from "redux";
import {RootState} from "../store/reducer";
import {toggleTheme} from "../store/module/theme/action";
import Brightness3Icon from '@material-ui/icons/Brightness3';
import Brightness5Icon from '@material-ui/icons/Brightness5';
import Drawer, {Action} from "./common/Drawer";

const mapStateToProps = (state: RootState) => ({theme: state.theme.current})

const mapDispatchToProps = (dispatch: Dispatch) => ({toggleTheme: () => dispatch(toggleTheme())})

const connector = connect(mapStateToProps, mapDispatchToProps);
type ReduxTypes = ConnectedProps<typeof connector>;

export interface Props {
}

class Application extends React.Component<Props & ReduxTypes> {

    render() {

        const noDrawer = (new URL(window.location.href)).searchParams.get("no_drawer") === "true"
        let component = <Manager/>;

        if (!noDrawer) {
            const actions: Action[] = [{
                icon: this.props.theme === "dark" ? <Brightness5Icon/> : <Brightness3Icon/>,
                text: "Toggle Theme",
                onClick: this.props.toggleTheme
            }]

            component = <Drawer position={"right"} actions={actions}>
                <Manager/>
            </Drawer>
        }

        return (
            <Paper square={true} className={"Application"}>
                {component}
            </Paper>
        );
    }
}

export default connector(Application)
