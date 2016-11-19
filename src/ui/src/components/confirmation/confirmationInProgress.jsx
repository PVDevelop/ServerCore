import React from "react";
import Panel from "react-bootstrap/lib/Panel";

export default class ConfirmationInProgress extends React.Component {
    render() {
        return (
            <Panel header="Ожидание подтверждения пользователя..."/>
        );
    }
}