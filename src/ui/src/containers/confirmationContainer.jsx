import React from "react";
import { connect } from "react-redux";
import { browserHistory } from "react-router";

import * as confirmUserActions from "../actions/confirmUser";
import Confirmation from "../components/confirmation";

class ConfirmationContainer extends React.Component {
    constructor(props) {
        super(props);

        this.onSuccessClicked = this.onSuccessClicked.bind(this);
        this.onRepeatClicked = this.onRepeatClicked.bind(this);
    }

    render() {
        return <Confirmation
            isConfirming={this.props.isConfirming}
            confirmationError={this.props.error}
            repeat={this.props.repeat}
            onSuccessClicked={this.onSuccessClicked}
            onRepeatClicked={this.onRepeatClicked} />;
    }

    componentDidMount() {
        this.props.dispatch(confirmUserActions.confirm(this.props.params.key));
    }

    onRepeatClicked() {
        this.props.dispatch(confirmUserActions.confirm(this.props.params.key));
    }

    onSuccessClicked() {
        browserHistory.push("/signIn");
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