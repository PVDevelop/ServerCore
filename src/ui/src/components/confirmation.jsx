import React from "react";

export default class Confirm extends React.Component {
    render() {
        return (
            <div>
                <h2>Окно подтверждения '{this.props.params.confirmation_id}'.</h2>
            </div>
        );
    }
}