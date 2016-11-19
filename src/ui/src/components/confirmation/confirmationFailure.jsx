import React from "react";
import { browserHistory } from "react-router";
import Panel from "react-bootstrap/lib/Panel";
import Button from "react-bootstrap/lib/Button";

export default class ConfirmationFailure extends React.Component {
    onOkClicked() {
        browserHistory.push('/');
    }

    render() {
        return (
            <Panel header="Ошибка подтверждения пользователя">
                <Button
                    onClick={::this.onOkClicked}>OK</Button>
            </Panel>
        );
    }
}