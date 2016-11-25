import React from "react";
import { browserHistory } from "react-router";
import { connect } from "react-redux";

import Panel from "react-bootstrap/lib/Panel";
import Form from "react-bootstrap/lib/Form";
import FormGroup from "react-bootstrap/lib/FormGroup";
import FormControl from "react-bootstrap/lib/FormControl";
import Col from "react-bootstrap/lib/Col";
import ControlLabel from "react-bootstrap/lib/ControlLabel";
import Button from "react-bootstrap/lib/Button";

import * as signInActions from "../actions/signIn";

class SignIn extends React.Component {
    render() {
        return (
            <Panel header="Вход в систему">
                <Form>
                    <FormGroup>
                        <Col sm={1} componentClass={ControlLabel}>Email</Col>
                        <Col sm={11}>
                            <FormControl
                                type="email"
                                placeholder="Введите почтовый адрес"
                                value={this.props.email}
                                onChange={::this.onEmailChanged} />
                        </Col>
                    </FormGroup>

                    <FormGroup>
                        <Col sm={1} componentClass={ControlLabel}>Пароль</Col>
                        <Col sm={11}>
                            <FormControl
                                type="password"
                                placeholder="Введите пароль"
                                value={this.props.password}
                                onChange={::this.onPasswordChanged} />
                        </Col>
                    </FormGroup>

                    <FormGroup>
                        <Col smOffset={1} sm={11}>
                            <Button
                                type="submit"
                                disabled={this.props.isSigningIn === true}
                                onClick={::this.onSignInClicked}>
                                Войти
                        </Button>
                        </Col>
                    </FormGroup>
                </Form>
            </Panel >
        );
    }

    onSignInClicked(e) {
        e.preventDefault();
        this.props.dispatch(signInActions.signIn(this.props.email, this.props.password));
    }

    onEmailChanged(e) {
        this.props.dispatch(signInActions.setEmail(e.target.value));
    }

    onPasswordChanged(e) {
        this.props.dispatch(signInActions.setPassword(e.target.value));
    }
}

function mapStateToProps(state) {
    return {
        email: state.user.email,
        password: state.user.password,
        isSigningIn: state.user.signingIn
    };
}

export default connect(mapStateToProps)(SignIn);