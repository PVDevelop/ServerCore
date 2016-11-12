import React from "react";
import { browserHistory } from "react-router";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";

import Form from "react-bootstrap/lib/Form";
import FormControl from "react-bootstrap/lib/FormControl";
import FormGroup from "react-bootstrap/lib/FormGroup";
import ControlLabel from "react-bootstrap/lib/ControlLabel";
import Button from "react-bootstrap/lib/Button";
import Panel from "react-bootstrap/lib/Panel";
import Col from "react-bootstrap/lib/Col";

import RegistrationState from "../../const/registration";
import RegistrationSuccessModal from "./registrationSuccessModal";
import RegistrationFailureModal from "./registrationFailureModal";
import * as registeringActions from "../../actions/registering";

class RegistrationForm extends React.Component {
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
                                onChange={::this.onEmailChange} />
                        </Col>
                    </FormGroup>

                    <FormGroup>
                        <Col sm={2} componentClass={ControlLabel}>Пароль</Col>
                        <Col sm={10}>
                            <FormControl
                                type="password"
                                placeholder="Введите пароль"
                                value={this.props.password}
                                onChange={::this.onPasswordChange} />
                        </Col>
                    </FormGroup>

                    <FormGroup>
                        <Col smOffset={2} sm={10}>
                            <Button
                                type="submit"
                                disabled={this.props.state === RegistrationState.PENDING}
                                onClick={::this.onRegisterButtonClicked}>Создать</Button>
                        </Col>
                    </FormGroup>

                    {this.props.state === RegistrationState.SUCCESS ? <RegistrationSuccessModal /> : null}
                    {this.props.state === RegistrationState.FAILURE ? <RegistrationFailureModal /> : null}
                </Form>
            </Panel>
        );
    }

    onEmailChange(event) {
        this.props.registrationActions.setEmail(event.target.value);
    }

    onPasswordChange(event) {
        this.props.registrationActions.setPassword(event.target.value);
    }

    onRegisterButtonClicked(e) {
        e.preventDefault();
        this.props.registrationActions.setState(RegistrationState.PENDING);

        var data = {
            email: this.props.email,
            password: this.props.password
        };

        var json = JSON.stringify(data);

        var options = {
            method: "post",
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json"
            },
            body: json
        };

        var url = "/api/users";

        fetch(url, options)
            .then(response => {
                if (response.status == 200) {
                    this.props.registrationActions.setState(RegistrationState.SUCCESS);
                }
                else {
                    this.props.registrationActions.setState(RegistrationState.FAILURE);
                }
            })
            .catch(err => {
                this.props.registrationActions.setState(RegistrationState.FAILURE);
            });
    }
}

function mapStateToProps(state) {
    return {
        email: state.registering.email,
        password: state.registering.password,
        state: state.registering.state
    };
}

function mapDispatchToProps(dispatch) {
    return {
        registrationActions: bindActionCreators(registeringActions, dispatch)
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(RegistrationForm);