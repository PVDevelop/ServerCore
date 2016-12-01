import React from "react";
import { connect } from "react-redux";
import { browserHistory } from "react-router";

import * as registerUserActions from "../actions/registerUser";
import Registration from "../components/registration";
import RegistrationSuccessModal from "../components/registrationSuccessModal";

class RegistrationContainer extends React.Component {
    constructor(props) {
        super(props);

        this.onEmailChanged = this.onEmailChanged.bind(this);
        this.onPasswordChanged = this.onPasswordChanged.bind(this);
        this.onConfirmPasswordChanged = this.onConfirmPasswordChanged.bind(this);
        this.onRegisterSubmitRequested = this.onRegisterSubmitRequested.bind(this);
        this.onRegistrationModalOkClicked = this.onRegistrationModalOkClicked.bind(this);
    }

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
                    hasInputErrors={this.props.hasInputErrors}
                    isRegistering={this.props.isRegistering}
                    registrationError={this.props.registrationError}
                    onEmailChanged={this.onEmailChanged}
                    onPasswordChanged={this.onPasswordChanged}
                    onConfirmPasswordChanged={this.onConfirmPasswordChanged}
                    onRegisterSubmitRequested={this.onRegisterSubmitRequested} />

                <RegistrationSuccessModal
                    show={this.props.showRegistrationSuccess}
                    onOkClicked={this.onRegistrationModalOkClicked} />
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
        hasInputErrors: state.registration.hasInputErrors,
        registrationError: state.registration.registrationError,
        isRegistering: state.registration.registering,
        showRegistrationSuccess: state.registration.registered
    };
}

export default connect(mapStateToProps)(RegistrationContainer);