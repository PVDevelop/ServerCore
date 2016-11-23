import React from "react";
import { bindActionCreators } from "redux";
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
import { httpPut } from "../utils/http";
import * as routes from "../routes";

class SignIn extends React.Component {
    componentWillMount() {
        this.props.signInActions.setIsCreating(false);
    }

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
                                onChange={::this.onEmailChange}/>
                        </Col>
                    </FormGroup>

                    <FormGroup>
                        <Col sm={1} componentClass={ControlLabel}>Пароль</Col>
                        <Col sm={11}>
                            <FormControl
                                type="password"
                                placeholder="Введите пароль"
                                value={this.props.password}
                                onChange={::this.onPasswordChange}/>
                        </Col>
                    </FormGroup>

                    <FormGroup>
                        <Col smOffset={1} sm={11}>
                            <Button type="submit" disabled={this.props.isCreating === true} onClick={::this.onCreateButtonClicked}>
                                Войти
                        </Button>
                        </Col>
                    </FormGroup>
                </Form>
            </Panel >
        );
    }

    onCreateButtonClicked(e) {
        e.preventDefault();
        this.props.signInActions.setIsCreating(true);

        var data = {
            email: this.props.email,
            password: this.props.password
        };

        httpPut(routes.SignIn, data)
            .then(response => {
                console.log(response);

                if (response.status == 200) {
                    alert("Вы успешно аутентифицировались.");
                    this.props.signInActions.setIsSignedIn(true);
                    browserHistory.push('/');
                }
                else {
                    alert("Ошибка аутентификации");
                }

                this.props.signInActions.setIsCreating(false);
            })
            .catch(err => {
                console.log(err);
                alert("Ошибка аутентификации");
                this.props.signInActions.setIsCreating(false);
            });
    }

    onEmailChange(event) {
        this.props.signInActions.setEmail(event.target.value);
    }

    onPasswordChange(event) {
        this.props.signInActions.setPassword(event.target.value);
    }
}

function mapStateToProps(state) {
    return {
        email: state.signIn.email,
        password: state.signIn.password,
        isCreating: state.signIn.isCreating
    };
}

function mapDispatchToProps(dispatch) {
    return {
        signInActions: bindActionCreators(signInActions, dispatch)
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(SignIn);