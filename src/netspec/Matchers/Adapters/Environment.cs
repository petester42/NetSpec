using System.Collections.Generic;

namespace NetSpec.Matchers
{

    /// "Global" state of Nimble is stored here. Only DSL functions should access / be aware of this
    /// class' existance
    internal class NimbleEnvironment
    {
        private AssertionHandler _assertionHandler;
        private static Dictionary<string, NimbleEnvironment> environments = new Dictionary<string, NimbleEnvironment>();

        public static NimbleEnvironment activeInstance
        {
            get
            {
                try
                {
                    var env = environments["NetSpecEnvironment"];

                    return env;
                }
                catch
                {
                    var newEnv = new NimbleEnvironment();
                    activeInstance = newEnv;
                    return newEnv;
                }
            }
            set
            {
                environments["NetSpecEnvironment"] = value;
            }
        }

        // TODO: eventually migrate the global to this environment value
        public AssertionHandler assertionHandler
        {
            get
            {
                return _assertionHandler;
            }
            set
            {
                _assertionHandler = value;
            }
        }

        // Awaiter awaiter;

        private NimbleEnvironment()
        {
            _assertionHandler = new NetSpecAssertionHandler();
            
            //TODO: fix awaiter
            //     let timeoutQueue: DispatchQueue
            //     if #available(OSX 10.10, *) {
            //     timeoutQueue = DispatchQueue.global(qos: .userInitiated)
            //     } else {
            //     timeoutQueue = DispatchQueue.global(priority: .high)
            // }

            // awaiter = Awaiter(
            //     waitLock: AssertionWaitLock(),
            //     asyncQueue: .main,
            //     timeoutQueue: timeoutQueue)
        }
    }
}