import React from "react";
import { connect } from "react-redux";

import Form from "react-bootstrap/lib/Form";
import FormControl from "react-bootstrap/lib/FormControl";
import FormGroup from "react-bootstrap/lib/FormGroup";
import ControlLabel from "react-bootstrap/lib/ControlLabel";
import Button from "react-bootstrap/lib/Button";
import Panel from "react-bootstrap/lib/Panel";
import Col from "react-bootstrap/lib/Col";

import * as registerUserActions from "../actions/registerUser";

class Registration extends React.Component {
    render() {
        return (
            <Panel header="Создание нового пользователя">
                <Form>
                    <FormGroup>
                        <Col sm={2} componentClass={ControlLabel}>Email</Col>
                        <Col sm={10}>
                            <FormControl
                                type="email"
                                placeholder="Введите почтовый адрес"
                                value={this.props.email}
                                onChange={::this.onEmailChanged} />
                        </Col>
                    </FormGroup>

                    <FormGroup>
                        <Col sm={2} componentClass={ControlLabel}>Пароль</Col>
                        <Col sm={10}>
                            <FormControl
                                type="password"
                                placeholder="Введите пароль"
                                value={this.props.password}
                                onChange={::this.onPasswordChanged} />
                        </Col>
                    </FormGroup>

                    <FormGroup>
                        <Col smOffset={2} sm={10}>
                            <Button
                                type="submit"
                                disabled={this.props.isRegistering === true}
                                onClick={::this.onRegisterButtonClicked}>
                                Создать
                            </Button>
                        </Col>
                    </FormGroup>
                </Form>
            </Panel >
        );
    }

    onEmailChanged(e) {
        this.props.dispatch(registerUserActions.onEmailChanged(e.target.value));
    }

    onPasswordChanged(e) {
        this.props.dispatch(registerUserActions.onPasswordChanged(e.target.value));
    }

    onRegisterButtonClicked(e) {
        e.preventDefault();
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

export default connect(mapStateToProps)(Registration);