import React from "react";
import { connect } from "react-redux";
import { browserHistory } from "react-router";

import * as registerUserActions from "../actions/registerUser";
import Registration from "../components/registration";
import RegistrationSuccessModal from "../components/registrationSuccessModal";

class RegistrationContainer extends React.Component {
    render() {
        return (
            <div>
                <Registration
                    email={this.props.email}
                    emailError={this.props.emailError}
                    passwordError={this.props.passwordError}
                    confirmPasswordError={this.props.confirmPasswordError}
                    password={this.props.password}
                    confirmPassword={this.props.confirmPassword}
                    hasErrors={this.props.hasErrors}
                    isRegistering={this.props.isRegistering}
                    onEmailChanged={::this.onEmailChanged}
                    onPasswordChanged={::this.onPasswordChanged}
                    onConfirmPasswordChanged={::this.onConfirmPasswordChanged}
                    onRegisterSubmitRequested={::this.onRegisterSubmitRequested}/>

                <RegistrationSuccessModal
                    show={this.props.showRegistrationSuccess}
                    onOkClicked={::this.onRegistrationModalOkClicked}/>
            </div>
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

    onRegistrationModalOkClicked() {
        browserHistory.push("/");
    }

    componentWillMount() {
        this.props.dispatch(registerUserActions.onRegisterComponentWillMount());
    }
}

function mapStateToProps(state) {
    return {
        email: state.registration.email,
        emailError: state.registration.emailError,
        password: state.registration.password,
        passwordError: state.registration.passwordError,
        confirmPassword: state.registration.confirmPassword,
        confirmPasswordError: state.registration.confirmPasswordError,
        hasErrors: state.registration.hasErrors,
        isRegistering: state.registration.registering,
        showRegistrationSuccess: state.registration.registered
    };
}

export default connect(mapStateToProps)(RegistrationContainer);