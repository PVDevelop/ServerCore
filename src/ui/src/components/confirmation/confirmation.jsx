import React from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";

import ConfirmationState from "../../const/confirmation";
import * as confirmingActions from "../../actions/confirming";

import ConfirmationInProgress from "./confirmationInProgress";
import ConfirmationSuccess from "./confirmationSuccess";
import ConfirmationFailure from "./confirmationFailure";

class Confirmation extends React.Component {
    render() {
        if (this.props.state === ConfirmationState.PENDING) {
            return <ConfirmationInProgress />
        }
        else if (this.props.state === ConfirmationState.SUCCESS) {
            return <ConfirmationSuccess />
        }
        else if (this.props.state === ConfirmationState.FAILURE) {
            return <ConfirmationFailure />
        }
        else {
            throw new Error("Unknown confirmation state " + this.props.state.name);
        }
    }

    componentDidMount() {
        console.log("Confirmation did mount");

        if (this.props.state === ConfirmationState.PENDING) {
            var options = {
                method: "get",
                credentials: "same-origin",
                headers: {
                    "Accept": "application/json"
                }
            };

            var url = "/api/confirmations/" + this.props.params.key;

            console.log("Sending confirmation at " + url);

            fetch(url, options)
                .then(response => {
                    console.log(response);

                    if (response.status == 200) {
                        this.props.confirmationActions.setState(ConfirmationState.SUCCESS);
                    }
                    else {
                        this.props.confirmationActions.setState(ConfirmationState.FAILURE);
                    }
                })
                .catch(err => {
                    console.log(err);
                    
                    this.props.confirmationActions.setState(ConfirmationState.FAILURE);
                });
        }
    }
}

function mapStateToProps(state) {
    return {
        state: state.confirming.state
    };
}

function mapDispatchToProps(dispatch) {
    return {
        confirmationActions: bindActionCreators(confirmingActions, dispatch)
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(Confirmation);