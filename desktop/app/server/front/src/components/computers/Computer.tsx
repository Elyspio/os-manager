import React, {Component} from 'react';
import {Box, ThemeProvider, Typography} from "@material-ui/core";
import "./Computer.scss"
import {Dispatch} from "redux";
import {connect, ConnectedProps} from "react-redux";
import Button from "@material-ui/core/Button";
import {ComputerService} from "../../core/ComputerService";
import {Computer as IComputer} from "../../../../../../../mobile/app/data/computer-manager/reducer";
import {RootState} from "../../store/reducer";
import createMuiTheme from "@material-ui/core/styles/createMuiTheme";

const mapStateToProps = (state: RootState) => ({
    theme: state.theme.current
})

const mapDispatchToProps = (dispatch: Dispatch) => ({})

const connector = connect(mapStateToProps, mapDispatchToProps);
type ReduxTypes = ConnectedProps<typeof connector>;


interface Props extends ReduxTypes {
    data: IComputer
}

const success = {borderColor: "green", color: "green"};
const warning = {borderColor: "orange", color: "orange"};
const danger = {borderColor: "red", color: "red"};

const btnTheme = createMuiTheme({
    palette: {
        primary: {main: '#aad7ff'},
        secondary: {main: '#b84444'},
    }
})

class Computer extends Component<Props> {
    render() {
        const data = this.props.data;
        const theme = this.props.theme;

        const btnVariant = theme === "dark" ? "outlined" : "contained"
        btnTheme.palette.type = theme;

        return (
            <Box className={`Computer ${theme}`}>
                <Typography className={"name"} title={data.host}>{data.name}</Typography>
                <div className="actions">
                    <ThemeProvider theme={btnTheme}>
                        <Button variant={btnVariant} color={"primary"} onClick={this.onLock}>Lock</Button>
                        <Button variant={btnVariant} color={"primary"} onClick={this.onSleep}>Sleep</Button>
                        <Button variant={btnVariant} color={"secondary"} onClick={this.onShutdown}>Shutdown</Button>
                        <Button variant={btnVariant} color={"secondary"} onClick={this.onReboot}>Reboot</Button>
                    </ThemeProvider>
                </div>
            </Box>
        );
    }


    private onSleep = () => {
        ComputerService.instance.sleep(this.props.data);
    }
    private onLock = () => {
        ComputerService.instance.lock(this.props.data);
    }
    private onReboot = () => {
        ComputerService.instance.reboot(this.props.data);
    }
    private onShutdown = () => {
        ComputerService.instance.shutdown(this.props.data)
    }
}

export default connector(Computer);
