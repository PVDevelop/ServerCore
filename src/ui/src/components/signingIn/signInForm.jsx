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

import * as signingInActions from "../../actions/signingIn";

class SignInForm extends React.Component {
    componentWillMount(){
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

            var json = JSON.stringify(data);

            var options = {
                method: "put",
                credentials: "same-origin",
                headers: {
                    "Accept": "application/json",
                    "Content-Type": "application/json"
                },
                body: json
            };

            var url = "/api/users";

            console.log("Sending user credentials at " + url);

            fetch(url, options)
                .then(response => {
                    console.log(response);

                    if (response.status == 200) {
                        browserHistory.push('/');
                    }
                    else {
                        alert("Fail!");
                    }

                    this.props.signInActions.setIsCreating(false);
                })
                .catch(err => {
                    console.log(err);
                    alert("Fail!");
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
        email: state.signingIn.email,
        password: state.signingIn.password,
        isCreating: state.signingIn.isCreating
    };
}

function mapDispatchToProps(dispatch) {
    return {
        signInActions: bindActionCreators(signingInActions, dispatch)
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(SignInForm);