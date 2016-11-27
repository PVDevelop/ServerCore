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
                confirmPassword={this.props.confirmPassword}
                isRegistering={this.props.isRegistering}
                onEmailChanged={::this.onEmailChanged}
                onPasswordChanged={::this.onPasswordChanged}
                onConfirmPasswordChanged={::this.onConfirmPasswordChanged}
                onRegisterSubmitRequested={::this.onRegisterSubmitRequested}/>
        );
    }

    onEmailChanged(email) {
        this.props.dispatch(registerUserActions.onEmailChanged(email));
    }

    onPasswordChanged(password) {
        this.props.dispatch(registerUserActions.onPasswordChanged(password));
    }

    onConfirmPasswordChanged(password) {
        this.props.dispatch(registerUserActions.onConfirmPasswordChanged(password));
    }

    onRegisterSubmitRequested() {
        this.props.dispatch(registerUserActions.register(this.props.email, this.props.password));
    }
}

function mapStateToProps(state) {
    return {
        email: state.registration.email,
        password: state.registration.password,
        confirmPassword: state.registration.confirmPassword,
        isRegistering: state.registration.registering
    };
}

export default connect(mapStateToProps)(RegistrationContainer);