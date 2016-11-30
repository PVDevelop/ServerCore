import React from "react";
import { connect } from "react-redux";
import { browserHistory } from "react-router";

import * as confirmUserActions from "../actions/confirmUser";
import Confirmation from "../components/confirmation";

class ConfirmationContainer extends React.Component {
    render() {
        return <Confirmation
            isConfirming={this.props.isConfirming}
            confirmationError={this.props.error}
            repeat={this.props.repeat}
            onSuccessClicked={::this.onSuccessClicked}
            onRepeatClicked={::this.onRepeatClicked}/>;
    }

    componentDidMount() {
        ::this.confirm();
    }

    onSuccessClicked(){
        browserHistory.push("/signIn");
    }

    onRepeatClicked(){
        ::this.confirm();
    }

    confirm(){
        this.props.dispatch(confirmUserActions.confirm(this.props.params.key));
    }
}

function mapStateToProps(state) {
    return {
        isConfirming: state.confirmation.confirming,
        error: state.confirmation.error,
        repeat: state.confirmation.repeat
    };
}

export default connect(mapStateToProps)(ConfirmationContainer);