import React from "react";
import { connect } from "react-redux";

import Registration from "../components/registration";
import * as registerUserActions from "../actions/registerUser";

class RegistrationContainer extends React.Component {
    render() {
        return (
            <Registration 
                email={this.props.email}
                password={this.props.password}
                isRegistering={this.props.isRegistering}
                onEmailChanged={::this.onEmailChanged}
                onPasswordChanged={::this.onPasswordChanged}
                onRegisterSubmitRequested={::this.onRegisterSubmitRequested}/>
        );
    }

    onEmailChanged(email) {
        this.props.dispatch(registerUserActions.onEmailChanged(email));
    }

    onPasswordChanged(password) {
        this.props.dispatch(registerUserActions.onPasswordChanged(password));
    }

    onRegisterSubmitRequested() {
        this.props.dispatch(registerUserActions.register(this.props.email, this.props.password));
    }
}

function mapStateToProps(state) {
    return {
        email: state.user.email,
        password: state.user.password,
        isRegistering: state.user.registering
    };
}

export default connect(mapStateToProps)(RegistrationContainer);